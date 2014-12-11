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
        private  int portNum = 5556; //per i comandi AT*PCMD, AT*REF
        private  Socket sender;


        private String cmd; //comando da inviare al drone 
        private String sentCmd; //conserva il valore di cmd dopo la chiamata a sendCmd() che imposta cmd a stringa nulla
        private int seq = 1; //num. seq del pkt udp
        private float speed = (float)0.25; //velocita movimento avanti/indietro
        private float lrSpeed = (float)0.5; //velocita movimento sx/dx e rotazione sx,dx
        private float MAX_SPEED = 10;


        //altre info. 
        private  bool connectedToDrone; //true se connectToDrone() ha avuto successo
        private  String logInfo; //contiene l'ultima riga di log


        //costruttore
        public ARDrone() {
            sender = null;
            cmd = "";
            sentCmd = "";
            logInfo = "drone non connesso";
            connectedToDrone = false;
        }



        //crea una connessione col drone
        public  void connectToDrone() {

            byte[] bytes = new byte[1024];


            //prova a connettersi
            try {
                IPAddress ipAddr = IPAddress.Parse(ardroneIP);
                IPEndPoint drone = new IPEndPoint(ipAddr, portNum);


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

            
            //imposta limite altezza
            setMaxAltitude(2);

            if (sendCmd())
            {
                connectedToDrone = true;
                Console.WriteLine("connesso al drone: " + ardroneIP + ":" + portNum);

                //comando per il video low-res
                cmd = "AT*CONFIG=" + (seq++) + ",\"video:video_codec\",\"128\"";
                sendCmd();

            }
            else
            {
                connectedToDrone = false;
                Console.WriteLine("non connesso al drone: " + ardroneIP + ":" + portNum);
            }

            
            
        } //connectToDrone



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
                Console.WriteLine("Impossibile inviare il mex. al drone!");
                return false;
            }
            catch (System.NullReferenceException e)
            {
                Console.WriteLine("Impossibile inviare il mex. al drone!");
                return false;
            }
            catch (Exception e) {
                Console.WriteLine("luca patti fa casini");
                return false;
            }


            sentCmd = cmd; //salvo il vecchio valore di cmd
            cmd = "";


            return true;
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

            cmd = "AT*CONFIG=1,\"control:altitude_max\",\"" + metres + "\"";
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
            cmd = "AT*PCMD=" + (seq++) + ",1," + intOfFloat(-lrSpeed) + ",0,0,0,";
        }

        public void moveRight()
        {
            Console.WriteLine("DESTRA");
            cmd = "AT*PCMD=" + (seq++) + ",1," + intOfFloat(lrSpeed) + ",0,0,0,";
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

        

    } //class ARDrone



}
