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
using System.IO;

namespace ARDroneTest
{
    class NavData
    {

        /* 
         * Struttura di un pacchetto contenente i navdata
         * LayoutKind.Seq specifica che in memoria i campi saranno nell'ordine di dichiarazione
         * Pack = 1 specifica l'allineamento sul singolo byte, cioè tutti i campi sono consecutivi, senza gap in memoria.
         * (l'assegnazione unsafe non è stata usata)
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct NavigationDataHeaderStruct
        {
            public uint Header;     //uint16
            public uint Status;     //uint32
            public uint SequenceNumber;
            public uint Vision;
        }


        /*
         * Struttura della "opzione"(opzione1) contenente i dati di navigazione
         */
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
            public int Altitude; //è in mm!!
            public Single VX;
            public Single VY;
            public Single VZ;
        }

        //oggetti usati per conservare i NAVDATA
        private NavigationDataHeaderStruct navDataHeaderStruct;
        public NavigationDataStruct navDataStruct;            //da aggiornare a ogni pkt ricevuto



        private Socket navdataSocket;
        private IPAddress ipAddr;
        private static int navdataPort = 5554;
        private string ardroneIP = "192.168.1.1";

        //private EndPoint drone;
        private IPEndPoint sender;

        private byte[] data;    //buffer contenente il payload del pkt ricevuto
        private bool dataReceived;  //true se ci sono dei nuovi navdata in data[], altrim. false se ci sono dati non aggiornati.



        //ritorna true quando ci sono dei nuovi dati aggiornati, se ci sono ancora i vecchi ritorna false
        public bool isNavdataAvailable() { return dataReceived; }



        //metodi getter
        public Single getBatteryLevel()
        {
            return navDataStruct.BatteryLevel;
        }

        public int getAltitude() {
            return navDataStruct.Altitude;
        }

        public Single getRoll() {
            return navDataStruct.Phi/(float)1000.0;
        }

        public Single getPitch() {
            return navDataStruct.Theta/(float)1000.0;
        }

        public Single getYaw() {
            return navDataStruct.Psi/(float)1000.0;
        }

        public Single getVX() {
            return navDataStruct.VX;
        }

        public Single getVY() {
            return navDataStruct.VY;
        }

        public Single getVZ() {
            return navDataStruct.VZ;
        }

        public uint getControlStatus() {
            return navDataStruct.ControlStatus;
        }



        //invia il pkt di wakeup per dire al drone di iniziare a 
        //inviare pkt NAVDATA
        public void initNavdata() {

            dataReceived = false;

            ipAddr = IPAddress.Parse(ardroneIP);


            sender = new IPEndPoint(IPAddress.Any, navdataPort);
            //drone = (EndPoint)sender;


            //invia pkt di navdataSocket
            byte[] wkpbuf = {1,0,0,0};

            navdataSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //navdataSocket.Bind(drone);
            
            navdataSocket.Connect(ipAddr, navdataPort);
            try {
                navdataSocket.Send(wkpbuf);
            } catch( SocketException se ) {
                Console.WriteLine( se.ToString() );
            }

            Console.WriteLine("wakeup inviato");

        }



        public  bool receiveNavdata() {

            //controlla se sono arrivati dei pkt
            //altrim. ritorna immediatamente
            if (navdataSocket.Available <= 0)
                return false;


            data = new byte[1024];

            int recv = 0;
            try {

                //legge i dati in un buffer. 
                //N.B: Receive è un metodo che blocca il thread.
                recv = navdataSocket.Receive(data);
                   
            } catch(Exception e) {
                Console.WriteLine(e.ToString());
                return false;
            }


            dataReceived = true;

            return true; //dati ricevuti corretamente
        }



        //chiude stream navdata
        private void disconnectNavdata() {
            navdataSocket.Close();
        }


        /* metodi per convertire byte ricevuti col socket
         * in interi signed e non e float(Single).*/

        //dato un buffer e startPos, prende i 4 byte da buf[startPos] a buf[startPos+3]
        //li trasforma in un uint a 32 bit e lo ritorna
        //se non riesce ritorna null.
        private uint getUint32FromBytes(byte[] buf, int startPos) {

            byte[] intBuf = { 0, 0, 0, 0 }; //creo un buf. temp. di 4 byte

            for( int i = 0; i < 4; i++) {
                intBuf[i] = buf[startPos+i]; 
            }

            
            //converto i 4 byte in un uint
            uint j = BitConverter.ToUInt32(intBuf, 0);


            return j;
        }


        //dato un buffer e startPos, prende i 4 byte da buf[startPos] a buf[startPos+3]
        //li trasforma in un int a 32 bit e lo ritorna
        //se non riesce ritorna null.
        private int getInt32FromBytes(byte[] buf, int startPos)
        {

            byte[] intBuf = { 0, 0, 0, 0 }; //creo un buf. temp. di 4 byte

            for (int i = 0; i < 4; i++)
            {
                intBuf[i] = buf[startPos + i];
            }

            //converto i 4 byte in un uint
            int j = BitConverter.ToInt32(intBuf, 0);


            return j;
        }



        //dato un buffer e startPos, prende i 2 byte da buf[startPos] a buf[startPos+1]
        //li trasforma in un ushort a 16 bit e lo ritorna
        //se non riesce ritorna null.
        private ushort getUshort16FromBytes(byte[] buf, int startPos)
        {

            byte[] intBuf = { 0, 0 }; //creo un buf. temp. di 4 byte

            intBuf[0] = buf[startPos];
            intBuf[1] = buf[startPos + 1];

            //converto i 2 byte in un uint
            ushort j = (ushort)BitConverter.ToInt16(intBuf, 0);


            return j;
        }


        //dato un buffer e startPos, prende i 4 byte da buf[startPos] a buf[startPos+1]
        //li trasforma in un float a 32 bit e lo ritorna
        //se non riesce ritorna null.
        //N.B: Single = float
        private Single getSingleFromBytes(byte[] buf, int startPos)
        {
            //drone codifica i float come interi, leggo prima come intero i 4 bytes
            int i = getInt32FromBytes(buf, startPos);


            byte[] floatBuf = { 0, 0, 0, 0 }; //creo un buf. temp. di 4 byte
            floatBuf = BitConverter.GetBytes(i);


            Single f = BitConverter.ToSingle(floatBuf, 0);
            return f;

        }



        //aggiorna le struttre dati navdataHeaderStruct e navdataStruct
        //se sono disponibili dei nuovi dati di navigazione, 
        //ritorna true se li aggiorna
        //ritorna false se non riesce/non sono disponibili dei nuovi dati di navigazione
        public bool updateNavigationData() {
            //ctrl. se sono disponibili dei dati di nav. aggiornati
            if (!dataReceived)
                return false;

            
            /*
             * header
             * status
             * seq. num.
             * vision
             */
            navDataHeaderStruct.Header = getUint32FromBytes(data, 0);
            navDataHeaderStruct.Status = getUint32FromBytes(data, 4);
            navDataHeaderStruct.SequenceNumber = getUint32FromBytes(data, 8);
            navDataHeaderStruct.Vision = getUint32FromBytes(data, 12);
            

            //controllo integrità header
            if (navDataHeaderStruct.Header != 0x55667788) {
                Console.WriteLine("Header NAVDATA errato! Ho ricevuto: " + navDataHeaderStruct.Header);
            }


            /*
             * N.B: qui sto supponendo le seguenti cose:
             *  -c'è sempre option1 al primo posto nel payload del pklt UDP
             *  -non mi interesso delle altre opzioni, un eventuale parsing di esso andrebbe aggiunto qui.
             *  
             * Per il parsing completo serve un while che salta itera tra le opzioni
             * salvando ognuno in una struttura option e funzioni di parsing per ognuna di esse.
             */


            /*
             * OPTION1:
             * Id(ushort)
             * Size(ushort)
             * Control STate
             * Battary %
             * pitch
             * roll
             * yaw
             * height
             * vx
             * vy
             * vz( sempre a 0 in demo mode, bisogna attivare info. dettagliate per averla)
             */
            navDataStruct.Tag = getUshort16FromBytes(data, 16);
            navDataStruct.Size = getUshort16FromBytes(data, 18);

            navDataStruct.ControlStatus = getUint32FromBytes(data, 20);
            navDataStruct.BatteryLevel = getUint32FromBytes(data, 24);

            navDataStruct.Theta = getSingleFromBytes(data, 28)/(Single)1000.0; //pitch
            navDataStruct.Phi = getSingleFromBytes(data, 32)/(Single)1000.0; //roll
            navDataStruct.Psi = getSingleFromBytes(data, 36)/(Single)1000.0; //yaw(ang. speed)

            navDataStruct.Altitude = getInt32FromBytes(data, 40);

            navDataStruct.VX = getSingleFromBytes(data, 44);
            navDataStruct.VY = getSingleFromBytes(data, 48);
            navDataStruct.VZ = getSingleFromBytes(data, 52);    //non disponibile in demo_mode
            

            //una volta parsati i dati disponibili diventano vecchi,
            //dataReceived ridiventerà true quando ne riceverò di nuovi dal drone
            dataReceived = false;

            return true;

        } //updateNavData




    } //class
} //namespace
