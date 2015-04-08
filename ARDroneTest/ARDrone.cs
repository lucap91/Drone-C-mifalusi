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
        private Socket sender;              //socket per i comandi di movimento        


        private String cmd; //comando da inviare al drone 
        private String sentCmd; //conserva il valore di cmd dopo la chiamata a sendCmd() che imposta cmd a stringa nulla
        private int seq = 1; //num. seq del pkt udp
        private float speed = (float)0.25; //velocita movimento avanti/indietro
        private float lrSpeed = (float)0.5; //velocita movimento sx/dx e rotazione sx,dx(è sempre il doppio di speed)
        private float MAX_SPEED = 10;


        //altre info. 
        private  bool connectedToDrone; //true se connectToDrone() ha avuto successo
        
        private Timer timer;       //timer usato per mandare mex. wakeup almeno ogni 500ms
        private Timer navdataTimer; //timer usato per leggere navdata
        private static int timerDuration = 500; //la funzione di callback viene chiamata da timer ogni timerDuration ms




        //costruttore
        public ARDrone() {
            sender = null;
            cmd = "";
            sentCmd = "";
            connectedToDrone = false;
        }



        //crea una connessione col drone
        public  void connectToDrone() {
            
            if (connectedToDrone) {
                return;
            }

            //prova a connettersi
            try {

                IPAddress ipAddr = IPAddress.Parse(ardroneIP);

                //crea socket
                sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                sender.Connect(ipAddr, portNum);
                
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
            
            
            //prova ad inviare un comando
            if (sendCmd())
            {
                connectedToDrone = true;
                Console.WriteLine("Connesso al drone: " + ardroneIP + ":" + portNum);
            }
            else
            {
                connectedToDrone = false;
                Console.WriteLine("Non connesso al drone: " + ardroneIP + ":" + portNum);
            }

            
            //avvia il timer che chiama la funzione per mandare i mex. di wakeup(hover) almeno
            //una volta ogni 500ms( dopo 2000ms il drone va in timeout).
            timer = new Timer(_ => sendWakeupCallback(), null, 0, timerDuration); //every timerDuration seconds
            
        } //connectToDrone 



        //chiude le connessioni col drone
        public void disconnectDrone() {

            sender.Disconnect(true);
            //navData.Disconnect(true);

        } //disconnect from drone



        public bool sendCmd() {

            //Console.WriteLine("AT command: " + cmd);

            // encoding: ASCII, aggiunge qui a ogni comando il carriage return <CR>
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
            //uso AT cmd di hovering per evitare che il drone vada in timeout
            hover();
            sendCmd();
        }



        //setter
        public void setSpeed( float s ) {
            if( s > 0 && s < MAX_SPEED ) {
                speed = s;
            }
        }

        public void setRotationSpeed(float rs) {
            if (rs > 0 && rs < MAX_SPEED) {
                lrSpeed = rs;
            }
        }



        //ritorna true se connesso al drone
        public bool isConnectedToDrone() { return connectedToDrone; }

        //ritorna l'ultimo comando impostato(cmd diventa stringa vuota in sendCmd())
        public String getCmd() { return cmd; }

        //ritorna l'ultimo comando inviato
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
        public void setMaxAltitude( int metres ) {
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
            //Console.WriteLine("HOVERING");
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
            cmd = "AT*PCMD=" + (seq++) + ",1,0,0," + intOfFloat(lrSpeed) + ",0";
        }
        
        public void moveDown()
        {
            Console.WriteLine("GIU'");
            cmd = "AT*PCMD=" + (seq++) + ",1,0,0," + intOfFloat(-lrSpeed) + ",0";
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

        
        //dice al drone di uscire dalla bootstrap mode e andare in demo mode
        //e quindi iniziare ad inviare a NAVDATA
        public void navdataDemoModeOn() {
            Console.WriteLine("esco dalla bootstrap");
            cmd = "AT*CONFIG=" + (seq++) + ",\"general:navdata_demo\",\"TRUE\"";
            Console.WriteLine(cmd);
        }



        /* NON FUNZIONA!!! */
        //animazioni,m da 0 a 19
        //parametri: num. animazione, durata  in s(se 0 usa la durata di default)
        /*public void playAnimation(int animNum) {

            if (animNum < 0 || animNum > 19)
            {
                Console.WriteLine("numero animazione fuori dai limiti![0-19]");
            }*/

            /*
             * num  durata     animazione
                0:  1000,  // ARDRONE_ANIM_PHI_M30_DEG
                1:  1000,  // ARDRONE_ANIM_PHI_30_DEG
                2:  1000,  // ARDRONE_ANIM_THETA_M30_DEG
                3:  1000,  // ARDRONE_ANIM_THETA_30_DEG
                4:  1000,  // ARDRONE_ANIM_THETA_20DEG_YAW_200DEG
                5:  1000,  // ARDRONE_ANIM_THETA_20DEG_YAW_M200DEG
                6:  5000,  // ARDRONE_ANIM_TURNAROUND
                7:  5000,  // ARDRONE_ANIM_TURNAROUND_GODOWN
                8:  2000,  // ARDRONE_ANIM_YAW_SHAKE
                9:  5000,  // ARDRONE_ANIM_YAW_DANCE
                10: 5000,  // ARDRONE_ANIM_PHI_DANCE
                11: 5000,  // ARDRONE_ANIM_THETA_DANCE
                12: 5000,  // ARDRONE_ANIM_VZ_DANCE
                13: 5000,  // ARDRONE_ANIM_WAVE
                14: 5000,  // ARDRONE_ANIM_PHI_THETA_MIXED
                15: 5000,  // ARDRONE_ANIM_DOUBLE_PHI_THETA_MIXED
             * 
             *  16,17,18,19: 15 //FLIP forward, backward, left, right
             */
            
            //durate di default delle singole animazioni
            /*int[] MAYDAY_TIMEOUT = {
                1000,
                1000,
                1000,
                1000,
                1000,
                1000,
                5000,
                5000,
                2000,
                5000,
                5000,
                5000,
                5000,
                5000,
                5000,
                5000
            };



            int duration = MAYDAY_TIMEOUT[animNum];

            Console.WriteLine("Animazione n. " + animNum);

            //cmd = "AT*CONFIG=" + (seq++) + ",\"control:flight_anim\",\"" + animNum + ",2000\"";
            cmd = "AT*CONFIG=" + (seq++) + ",\"control:flight_anim\",\"" + animNum + "," + duration + "\"";
            Console.WriteLine(cmd);
        }*/
        

        /* NON FUNZIUONA */
        //animazioni dei led
        //parametri: num. animazione, frequenza e durata
        /*public void playLedAnimation(int animNum) {
            if (animNum < 0 || animNum > 13) {
                Console.WriteLine("numero animazione fuori dai limiti![0-13]");
            }


            float freq = (float)3;
            float duration = 2;

            Console.WriteLine("Animazione LED n. " + animNum + ", freq=" + freq + ", durata=" + duration);

            //cmd = "AT*CONFIG=" + (seq++) + ",\"leds:leds_anim\",\"" + animNum + "," + intOfFloat(freq) + "," + duration + "\"";
            //cmd = "AT*CONFIG=" + (seq++) + ",\"leds:leds_anim\",\"" + animNum + "," + intOfFloat(freq) + "," + duration + "\"";
            
                cmd = "AT*CONFIG=" + (seq++) + ",\"leds:leds_anim\",\"1,1073741824,2\"";
            Console.WriteLine(cmd);
        }*/
        

    } //class ARDrone



}
