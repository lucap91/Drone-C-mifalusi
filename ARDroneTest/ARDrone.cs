using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;


namespace ARDroneTest

{
    class ARDrone
    {
       //config. rete parrot
        private  String ardroneIP = "192.168.1.1";
        private  int portNum = 5556;        //per i comandi AT*PCMD, AT*REF
        private int videoPortNum = 5555;    //per wakeup del video stream?
        private int navDataPortNum = 5554;

        private Socket sender;              //socket per i comandi di movimento
        //private Socket videoStreamWakeup;   //socket per il pacchetti di wakeup del videostream
        private Socket navData;             //socket per ricevere nav data dal drone


        private String cmd; //comando da inviare al drone 
        private String sentCmd; //conserva il valore di cmd dopo la chiamata a sendCmd() che imposta cmd a stringa nulla
        private int seq = 1; //num. seq del pkt udp
        private float speed = (float)0.25; //velocita movimento avanti/indietro
        private float lrSpeed = (float)0.5; //velocita movimento sx/dx e rotazione sx,dx
        private float MAX_SPEED = 10;


        //altre info. 
        private  bool connectedToDrone; //true se connectToDrone() ha avuto successo
        private  String logInfo; //contiene l'ultima riga di log
        
        private Timer timer;       //timer usato per mandare mex. wakeup almeno ogni 500ms
        private Timer navdataTimer; //timer usato per leggere navdata
        private static int timerDuration = 500; //la funzione di callback viene chiamata da timer ogni timerDuration ms


    //usati per la lettura async. dei NAVDATA
        private static ManualResetEvent sendDone;
        private static ManualResetEvent navdataConnectDone;
        byte[] rawNavdata = new byte[256];







        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct NavigationDataHeaderStruct
        {
            public uint Header;
            public uint Status;
            public uint SequenceNumber;
            public uint Vision;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct NavigationDataStruct
        {
            public ushort Tag;
            public ushort Size;
            public uint ControlStatus;
            public uint BatteryLevel;
            public Single Theta;
            public Single Phi;
            public Single Psi;
            public int Altitude;
            public Single VX;
            public Single VY;
            public Single VZ;
        }



        private NavigationDataHeaderStruct currentNavigationDataHeaderStruct;
        private NavigationDataStruct currentNavigationDataStruct;


        //costruttore
        public ARDrone() {
            sender = null;
            cmd = "";
            sentCmd = "";
            logInfo = "drone non connesso";
            connectedToDrone = false;

            navdataConnectDone = new ManualResetEvent(false);
        }



        //crea una connessione col drone
        public  void connectToDrone() {
            
            if (connectedToDrone) {
                logInfo = "Già connesso al drone";
                return;
            }

            //prova a connettersi
            try {
                //togliere???
                IPAddress ipAddr = IPAddress.Parse(ardroneIP);
                IPEndPoint remoteEP = new IPEndPoint(ipAddr, navDataPortNum); //usato per la lettura asinc. dello stream NAVDATA
                /*IPEndPoint drone = new IPEndPoint(ipAddr, portNum);
                IPEndPoint droneVideoWakeup = new IPEndPoint(ipAddr, videoPortNum); //TEST
                IPEndPoint droneNavData = new IPEndPoint(ipAddr, navDataPortNum);//TEST
                */


                //crea socket
                sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                sender.Connect(ipAddr, portNum);


                navData = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                navData.Connect(ipAddr, portNum);
                byte[] init = { 1,0,0,0};
                navData.Send( init );

                //navData.BeginConnect(remoteEP, new AsyncCallback(receiveNavdataCallback), navData);
                //navdataConnectDone.WaitOne();

                //Send(navData, "This is a test<EOF>");
                //sendDone.WaitOne();

                
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("Arg. null. exception", ane.ToString());
                return;
            }
            catch (SocketException se)
            {
                Console.WriteLine("Socket exception!", se.ToString());
                return;
            }
            catch (Exception e) {
                Console.WriteLine("eccezione non prevista :|", e.ToString());
                return;
            }
            
            
            //imposta limite altezza
            setMaxAltitude(3);

            if (sendCmd())
            {
                connectedToDrone = true;
                Console.WriteLine("connesso al drone: " + ardroneIP + ":" + portNum);

                
                //cmd = "AT*PMODE=" + (seq++) + ",2";


                //cmd = "AT*CONFIG=\"general:navdata_demo\",\"TRUE\"";
                //sendCmd();


            }
            else
            {
                connectedToDrone = false;
                Console.WriteLine("non connesso al drone: " + ardroneIP + ":" + portNum);
            }

            
            //avvia il timer che chiama la funzione per mandare i mex. di wakeup almeno
            //una volta ogni 1000ms( dopo 2000ms il drone va in timeout).
            timer = new Timer(_ => sendWakeupCallback(), null, 0, timerDuration); //every timerDuration seconds


            
            
        } //connectToDrone 



        //chiude le connessioni col drone
        public void disconnectDrone() {

            sender.Disconnect(true);
            navData.Disconnect(true);

        } //disconnect from drone



        public bool sendCmd() {

            Console.WriteLine("AT command: " + cmd);

            //###### encoding: ASCII o UTF8???
            byte[] buffer = Encoding.ASCII.GetBytes((cmd + "\r"));

            try
            {
                
                sender.Send(buffer);

            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: Impossibile inviare il mex. al drone!");
                return false;
            }
            catch (System.NullReferenceException e)
            {
                Console.WriteLine("NullReferenceException: Impossibile inviare il mex. al drone!");
                return false;
            }
            catch (Exception e) {
                Console.WriteLine("Exception: Impossibile inviare il mex. al drone!");
                return false;
            }


            sentCmd = cmd; //salvo il vecchio valore di cmd
            cmd = "";

            return true;
        }




        //Invia ATCMD per hovering, questa funz. va chiamata almeno 1 volta ogni 2000ms
        //altrimenti il drone va in timeout e interrompe tutte le comunicazioni(flusso video compreso)
        //N.B. messaggio di wakeup = AT CMD per hovering. Funziona sia con il drone a terra che in volo.
        private void sendWakeupCallback()
        {

            //fermo il timer per evitare che possa scattare nuovamente il callback
            //prima di aver inviato il comando di hovering
            //timer.Change(Timeout.Infinite, Timeout.Infinite);

            hover();
            sendCmd();

            //timer.Change(0, timerDuration);  //restarts the timer

        }



        //riempe buffer coi dati ricevuti 
        public uint ReceiveData()
        {
            byte[] buffer = new byte[256];
            int errCode = 0;
            try
            {
                if (navData != null)
                    /* NON FUNZIONA!!  */
                    errCode = navData.Receive(buffer);
            }
            catch (SocketException e)
            {
                Console.WriteLine("ReceiveData(): eccezione!");
            }


            unsafe
            {
                fixed (byte* entry = &buffer[0])
                {
                    currentNavigationDataHeaderStruct = *(NavigationDataHeaderStruct*)entry;
                }
            }
            //SetStatusFlags(currentNavigationDataHeaderStruct.Status);


            Console.WriteLine( currentNavigationDataHeaderStruct.Header );

            return currentNavigationDataHeaderStruct.Header;
        }




        //setter
        public void setSpeed( float s ) {
            if( s > 0 && s < MAX_SPEED ) {
                speed = s;
            }
        }


        //getter
        public String getLogInfo() { return logInfo; }
        public bool isConnectedToDrone() { return connectedToDrone; }
        public String getCmd() { return cmd; }
        public String getSentCmd() { return sentCmd; }

        
        /* Conversione float -> int
         * The number 􀀀0:8 is stored in memory as a 32-bit word whose value is BF4CCCCD(16), 
         * according to the IEEE-754 format. This 32-bit word can be considered as holding 
         * the 32-bit integer value 􀀀1085485875(10). So the command to send will be 
         * AT*PCMD=xx,xx,􀀀1085485875,xx,xx.
         */
        public int intOfFloat(float f)
        {
 
            //scompone il float nei 4 byte che lo compongono
            byte[] buffer = BitConverter.GetBytes( f );

            //interpreta i 4 byte come un intero e ritorna
            return BitConverter.ToInt32(buffer, 0); 
        }


        //funzioni per settare cmd, chiamare sendCmd dopo
        public void setMaxAltitude(int metres ) {
            if (metres <= 0)
            {
                metres = 1;
                Console.WriteLine("WARNING: altitudine max. settata a 1m");
            }

            //il comando prende l'altitudine in millimetri!!!
            cmd = "AT*CONFIG=1,\"control:altitude_max\",\"" + (metres*1000) + "\"";
        }


        //calibrazione = assetto piatto
        public void calibrate()
        {
            Console.WriteLine("CALIBRAZIONE");
            cmd = "AT*FTRIM=" + (seq++);
        }




        //hovering
        public void hover() {
            Console.WriteLine("HOVERING");
            cmd = "AT*PCMD=" + (seq++) + ",1,0,0,0,0"; 
        }

        //decollo/atterraggio
        public void takeoff() {
            Console.WriteLine("DECOLLO");
            cmd = "AT*REF=" + (seq++) + ",290718208";

            //invia wakeup video stream
            //sendVideoStreamWakeup();
        }

        public void land() {
            Console.WriteLine("ATTERRAGGIO");
            cmd = "AT*REF=" + (seq++) + ",290717696";
        }

        //avanti, indietro, sx, dx
        public void moveForward() {
            Console.WriteLine("AVANTI");
            cmd = "AT*PCMD=" + (seq++) + ",1,0," + intOfFloat(-speed) + ",0,0";
        }

        public void moveBackward() {
            Console.WriteLine("INDIETRO");
            cmd = "AT*PCMD=" + (seq++) + ",1,0," + intOfFloat(speed) + ",0,0";
        }

        public void moveLeft()
        {
            Console.WriteLine("SINISTRA");
            cmd = "AT*PCMD=" + (seq++) + ",1," + intOfFloat(-lrSpeed) + ",0,0,0";
        }

        public void moveRight()
        {
            Console.WriteLine("DESTRA");
            cmd = "AT*PCMD=" + (seq++) + ",1," + intOfFloat(lrSpeed) + ",0,0,0";
        }


        //quota
        public void moveUp()
        {
            Console.WriteLine("SU");
            cmd = "AT*PCMD=" + (seq++) + ",1,0,0," + intOfFloat(speed) + ",0";
        }
        
        public void moveDown()
        {
            Console.WriteLine("GIU'");
            cmd = "AT*PCMD=" + (seq++) + ",1,0,0," + intOfFloat(-speed) + ",0";
        }


        //rotazione
        public void rotateLeft()
        {
            Console.WriteLine("ROTAZIONE SX'");
            cmd = "AT*PCMD=" + (seq++) + ",1,0,0,0," + intOfFloat(-lrSpeed);
        }

        public void rotateRight()
        {
            Console.WriteLine("ROTAZIONE DX'");
            cmd = "AT*PCMD=" + (seq++) + ",1,0,0,0," + intOfFloat(lrSpeed);
        }


        //animazioni,m da 0 a 19
        //parametri: num. animazione, durata  in s(se 0 usa la durata di default)
       /* public void playAnimation(int animNum) {
            if (animNum < 0 || animNum > 19) {
                Console.WriteLine("numero animazione fuori dai limiti![0-19]");
            }

            Console.WriteLine("Animazione n. " + animNum);

            //cmd = "AT*CONFIG=" + (seq++) + ",\"control:flight_anim\",\"" + animNum + ",2000\"";
            cmd = "AT*CONFIG=" + (seq++) + ",\"control:flight_anim\",\"" + 3 + "," + 2 + "\"";
            Console.WriteLine(cmd);
        }
        

        //animazioni dei led
        //parametri: num. animazione, frequenza e durata
        public void playLedAnimation(int animNum, float freq, int duration) {
            if (animNum < 0 || animNum > 13) {
                Console.WriteLine("numero animazione fuori dai limiti![0-13]");
            }

            Console.WriteLine("Animazione LED n. " + animNum + ", freq=" + freq + ", durata=" + duration);

            //cmd = "AT*CONFIG=" + (seq++) + ",\"leds:leds_anim\",\"" + animNum + "," + intOfFloat(freq) + "," + duration + "\"";
            cmd = "AT*LED=" + (seq++) + "," + animNum + "," + intOfFloat(freq) + "," + duration;
            Console.WriteLine(cmd);
        }*/
        

    } //class ARDrone



}
