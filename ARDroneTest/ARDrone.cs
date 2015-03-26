﻿using System;
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
        private Socket videoStreamWakeup;   //socket per il pacchetti di wakeup del videostream
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
        
        private Timer timer;       //timer usato per mandare mex. wakeup almeno ogni 2000ms
        private static int timerDuration = 500; //la funzione di callback viene chiamata da timer ogni timerDuration ms


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
            
            if (connectedToDrone) {
                logInfo = "Già connesso al drone";
                return;
            }

            //prova a connettersi
            try {
                //togliere???
                IPAddress ipAddr = IPAddress.Parse(ardroneIP);
                IPEndPoint drone = new IPEndPoint(ipAddr, portNum);
                IPEndPoint droneVideoWakeup = new IPEndPoint(ipAddr, videoPortNum); //TEST
                IPEndPoint droneNavData = new IPEndPoint(ipAddr, navDataPortNum);//TEST


                //crea socket
                sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                sender.Connect(ipAddr, portNum);

                //TEST crea socket per pacchetti wakeup stream video
                //videoStreamWakeup = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //videoStreamWakeup.Connect(ipAddr, videoPortNum);

                //TEST crea socket per ricevere navdata
               /* navData = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                navData.Connect(ipAddr, navDataPortNum);*/
                
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

                //
                cmd = "AT*PMODE=" + (seq++) + ",2";
                //sendCmd();


                //cmd = "AT*MISC=" + (seq++) + ",20,2000,3000";
                //sendCmd();


                //assetto piatto(calibra orientamento iniziale drone)
                //cmd = "AT*FTRIM=" + (seq++);
                //sendCmd();


                //inizializza video stream - wakeup pkts vanno mandati in questo ordine
                //sendVideoStreamWakeup();

                //cmd = "AT*CONFIG=" + (seq++) + ",\"general:video_enable\",\"TRUE\"";
                //sendCmd();

                //sendVideoStreamWakeup();

                //cmd = "AT*CONFIG=" + (seq++) + ",\"video:bitrate_ctrl_mode\",\"0\"";
                //sendCmd();


                //cmd = "AT*CONFIG=" + (seq++) + ",\"video:video_codec\",\"128\"";
                //sendCmd();

            }
            else
            {
                connectedToDrone = false;
                Console.WriteLine("non connesso al drone: " + ardroneIP + ":" + portNum);
            }

            
            //avvia il timer che chiama la funzione per mandare i mex. di wakeup almeno
            //una volta ogni 1000ms( dopo 2000ms il drone va in timeout).
            timer = new Timer(_ => sendWakeupCallback(), null, 0, timerDuration); //every 10 seconds
            
            
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


        //TEST
        /*public bool sendVideoStreamWakeup() {

            byte[] buffer = { 0x01, 0x00, 0x00, 0x00 };

            try {
                videoStreamWakeup.Send(buffer);
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
            catch (Exception e)
            {
                Console.WriteLine("Exception: Impossibile inviare il mex. al drone!");
                return false;
            }


            return true;

        }*/



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
        public void playAnimation(int animNum) {
            if (animNum < 0 || animNum > 19) {
                Console.WriteLine("numero animazione fuori dai limiti![0-19]");
            }

            Console.WriteLine("Animazione n. " + animNum);

            cmd = "AT*CONFIG=" + (seq++) + ",\"control:flight_anim\",\"" + animNum + ",2000\"";
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
        }
        

    } //class ARDrone



}
