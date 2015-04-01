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
using System.IO;

namespace ARDroneTest
{
    class NavData
    {

        /* 
         * Struttura di un pacchetto contenente i navdata
         * LayoutKind.Seq specifica che in memoria i campi saranno nell'ordine di dichiarazione
         * Pack = 1 specifica l'allineamento sul singolo byte, cioè tutti i campi sono consecutivi, senza gap in memoria.
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
            public int Altitude;
            public Single VX;
            public Single VY;
            public Single VZ;
        }




        private NavigationDataHeaderStruct currentNavigationDataHeaderStruct;
        public NavigationDataStruct currentNavigationDataStruct;            //da aggiornare a ogni pkt ricevuto


        private Socket wakeup;
        IPAddress ipAddr;
        private static int navdataPort = 5554;
        private string ardroneIP = "192.168.1.1";

        //IPEndPoint iep;
        //IPEndPoint local;   //ip e porta su questo pc
        //EndPoint ep;
        EndPoint drone;
        IPEndPoint sender;

        byte[] data;    //buffer contenente il payload del pkt ricevuto
        private uint checksum;


        public void initNavdata() {

            ipAddr = IPAddress.Parse(ardroneIP);


            sender = new IPEndPoint(IPAddress.Any, navdataPort);
            drone = (EndPoint)sender;


            //invia pkt di wakeup
            byte[] wkpbuf = {1,0,0,0};

            wakeup = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            wakeup.Bind(drone);
            
            wakeup.Connect(ipAddr, navdataPort);
            try {
                wakeup.Send(wkpbuf);
            } catch( SocketException se ) {
                Console.WriteLine( se.ToString() );
            }

            Console.WriteLine("wakeup inviato");

        }



        public  void receiveNavdata(IAsyncResult ar) {

           Console.WriteLine("Ready to receive…");
            data = new byte[1024];

            int recv = 0;
            try {
                recv = wakeup.Receive(data);
                   
            } catch(Exception e) {
                Console.WriteLine(e.ToString());
            }


            string stringData = BitConverter.ToString(data);
            Console.WriteLine("received: {0}: ",  stringData);        
          

        }



        //chiude stream navdata
        private void disconnectNavdata() {
            wakeup.Close();
        }



        private void updateNavdataHeader() {

            unsafe {
                fixed (byte* entry = &data[0]) {
                    currentNavigationDataHeaderStruct = *(NavigationDataHeaderStruct*)entry;
                }
            }

        }


        private void determineNavigationData(int position) {
            unsafe {
                fixed (byte* entry = &data[position]) {
                    currentNavigationDataStruct = *(NavigationDataStruct*)entry;
                }
            }
        }


        public void updateNavigationData() {

            MemoryStream memoryStream;
            BinaryReader reader;

            memoryStream = new MemoryStream(data);
            reader = new BinaryReader(memoryStream);


            memoryStream.Position = Marshal.SizeOf(typeof(NavigationDataHeaderStruct));



            while( memoryStream.Position < memoryStream.Length ) {
                ushort tag = reader.ReadUInt16();
                ushort size = reader.ReadUInt16();

                if( tag == 0 ) {
                    determineNavigationData((int)(memoryStream.Position - 4));
                    memoryStream.Position += size - 4;
                }
                else if(tag == 0xFFFF) {
                    checksum = reader.ReadUInt32();
                }
                else {
                    memoryStream.Position += size - 4;
                }

            } //while

        } //updateNavData



        public uint getBatteryLevel()
        {
            return currentNavigationDataStruct.BatteryLevel;
        }


    } //class
} //namespace
