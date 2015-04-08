using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ARDroneTest
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow( IntPtr hWnd,  int nCmdShow );

        
        //processo finestra video
        Process ffplay;
        bool ffplayProcessStarted; //true se ffplay è stato avviatoed è in execuz.


        //oggetto drone
        private static ARDrone drone;

        //riceve i navdata
        private static NavData nav;

        //timer usati per chiamare periodicamente funzioni callback x aggiornare dati
        private System.Threading.Timer timer;
        private System.Threading.Timer timer2;



        public Form1()
        {
            InitializeComponent();
            ffplayProcessStarted = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //crea oggetto drone
            drone = new ARDrone();

            statusStrip.Text = "Drone non connesso.";

            //iniz. oggetto navdata
            nav = new NavData();
        }

        private void connettiButton_Click(object sender, EventArgs e)
        {
            //prova connessione
            drone.connectToDrone();
            

            if (drone.isConnectedToDrone() )
            {
                status.Text = "Connesso al drone!";

                //drone.sendVideoStreamWakeup();
                showVideo();
            }
            else {
                status.Text = "Connessione non riuscita!";
            }



            //manda pkt di wakeup per iniziare lo stream navdata
            nav.initNavdata(); //mando pkt wakeup


            //imposta un timeout prima di mandare AT command per uscire da bootstrap mode
            //ed entrare in demo mode per ricevere i navdata completi
            //lo chiamo in 1000ms in modo da dare al drone un pò di tempo
            timer = new System.Threading.Timer(_ => exitBootstrapEnterDemoMode(), null, 1000, System.Threading.Timeout.Infinite );
            

            //imposta un altro timeout per iniziare l'aggiornamento periodico dei navdata
            //chiama navdataPeriodicUpdate() dopo 500ms e poi ogni 5ms fino al termine del programma
            timer2 = new System.Threading.Timer(_ => navdataPeriodicUpdate(), null, 2000, 5);
            
        }

        private void showVideo()
        {
            // start ffplay 
            ffplay = new Process
            {
                StartInfo =
                {
                    FileName = "ffplay",
                    Arguments = "-f h264 tcp://192.168.1.1:5555",
                    // hides the command window
                    CreateNoWindow = true,
                    // redirect input, output, and error streams..
                    RedirectStandardError = false, /* da lasciare false entrambi altrim. la finestra parte quando si chiude il form */
                    RedirectStandardOutput = false,
                    UseShellExecute = false /* mostra/nasconde la shell che avvia ffplay.exe */
                }
            };

            ffplay.EnableRaisingEvents = true;
            ffplay.OutputDataReceived += (o, e) => Debug.WriteLine(e.Data ?? "NULL", "ffplay");
            ffplay.ErrorDataReceived += (o, e) => Debug.WriteLine(e.Data ?? "NULL", "ffplay");
            ffplay.Exited += (o, e) => Debug.WriteLine("Exited", "ffplay");


            //se il proc. è stato avviato bene Start() ritorna true
            try
            {
                if (ffplay.Start())
                {
                    ffplayProcessStarted = true;

                    Thread.Sleep(3000); //aspetta che il thread parta


                    //fa diventare questa finestra "parent" di quella di ffplay appena avviata
                    SetParent(ffplay.MainWindowHandle, this.Handle);


                    //finestra di ffplay: posizione x,y, width, height, repaint = true
                   MoveWindow(ffplay.MainWindowHandle, 713, 314, 320, 280, true);
                   
                }
                else
                {
                    MessageBox.Show("impossibile aprire la finestra video!");
                }

            }
            catch (Win32Exception we) {
                status.Text = "Impossibile avviare ffplay/aprire il video!";
            }

        }


        //pulsante decollo
        private void takeoffButton_Click(object sender, EventArgs e)
        {
            if (!drone.isConnectedToDrone()) {
                status.Text = "Connettersi al drone prima.";
                return;
            }
        

            drone.takeoff();
            
            if (drone.sendCmd())
            {
                status.Text = "Decollo effettuato" + " - comando inviato: " + drone.getSentCmd(); ;
            }

        }

        //pulsante atterraggio
        private void landButton_Click(object sender, EventArgs e)
        {
            if (!drone.isConnectedToDrone())
            {
                status.Text = "Connettersi al drone prima.";
                return;
            }


            drone.land();

            if (drone.sendCmd())
            {
                status.Text = "Atterraggio" + " - comando inviato: " + drone.getSentCmd(); ;
            }

        }

        //pulsanti movimento:
        private void moveUpButton_Click(object sender, EventArgs e)
        {
            if (!drone.isConnectedToDrone())
            {
                status.Text = "Connettersi al drone prima.";
                return;
            }

            drone.moveForward();

            if (drone.sendCmd()) {
                status.Text = "Avanti" + " - comando inviato: " + drone.getSentCmd(); ;
            }
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            if (!drone.isConnectedToDrone())
            {
                status.Text = "Connettersi al drone prima.";
                return;
            }

            drone.moveBackward();

            if (drone.sendCmd())
            {
                status.Text = "Indietro" + " - comando inviato: " + drone.getSentCmd(); ;
            }
        }

        private void buttonMoveLeft_Click(object sender, EventArgs e)
        {
            if (!drone.isConnectedToDrone())
            {
                status.Text = "Connettersi al drone prima.";
                return;
            }

            drone.moveLeft();

            if (drone.sendCmd())
            {
                status.Text = "Sinistra" + " - comando inviato: " + drone.getSentCmd(); ;
            }
        }

        private void buttonMoveRight_Click(object sender, EventArgs e)
        {
            if (!drone.isConnectedToDrone())
            {
                status.Text = "Connettersi al drone prima.";
                return;
            }

            drone.moveRight();

            if (drone.sendCmd())
            {
                status.Text = "Destra" + " - comando inviato: " + drone.getSentCmd(); ;
            }
        }

        private void buttonGoUp_Click(object sender, EventArgs e)
        {
            if (!drone.isConnectedToDrone())
            {
                status.Text = "Connettersi al drone prima.";
                return;
            }

            drone.moveUp();

            if (drone.sendCmd())
            {
                status.Text = "Su" + " - comando inviato: " + drone.getSentCmd(); ;
            }

        }

        private void buttonGoDown_Click(object sender, EventArgs e)
        {
            if (!drone.isConnectedToDrone())
            {
                status.Text = "Connettersi al drone prima.";
                return;
            }

            drone.moveDown();

            if (drone.sendCmd())
            {
                status.Text = "Giù" + " - comando inviato: " + drone.getSentCmd();
            }
        }

        //chiamata automaticam. alla chiusura del form, chiude la finestra di ffplay
        //prima di uscire, altrimenti resta aperta essendo un processo separato.
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //se chiudo ffplay e poi il form ho un eccezione NullPointer
            if ( ffplay != null && !ffplay.HasExited ) {
                ffplay.CloseMainWindow();
                ffplay.Close();
            }

        }


        //Assetto piatto per calibrare giroscopio.
        private void buttonCalibra_Click(object sender, EventArgs e)
        {
            if (!drone.isConnectedToDrone())
            {
                status.Text = "Connettersi al drone prima.";
                return;
            }


            drone.calibrate();

            if(drone.sendCmd())
            {
                status.Text = "drone calibrato.";

            }
            else {
                status.Text = "drone NON calibrato.";
            }
        }


        //Gestione comandi da tastiera:
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!drone.isConnectedToDrone())
            {
                status.Text = "Connettersi al drone prima.";
                return;
            }

            /*
             * Tasti:
             * W,A,S,D : avanti, sx, indietro, dx
             * Q,E: rotazione sx, dx
             * P,L: sali,scendi
             */
            if (e.KeyCode == Keys.W)
            {
                drone.moveForward();
            }
            else if (e.KeyCode == Keys.S)
            {
                drone.moveBackward();
            }
            else if (e.KeyCode == Keys.A)
            {
                drone.moveLeft();
            }
            else if (e.KeyCode == Keys.D)
            {
                drone.moveRight();
            }
            else if (e.KeyCode == Keys.P)
            {
                drone.moveUp();
            }
            else if (e.KeyCode == Keys.L)
            {
                drone.moveDown();
            }
            else if (e.KeyCode == Keys.Q)
            {
                drone.rotateLeft();
            }
            else if (e.KeyCode == Keys.E)
            {
                drone.rotateRight();
            }
            else
            {
                //se si preme un tasto non riconosciuto mando un hover
                drone.hover();
            }

            status.Text = "Comando inviato: " + drone.getCmd();
            drone.sendCmd();
            
        }

        //pulsanti rotazione drone:
        private void rotateLeft_Click(object sender, EventArgs e)
        {
            drone.rotateLeft();
            drone.sendCmd();
        }
        
        private void rotateRight_Click(object sender, EventArgs e)
        {
            drone.rotateRight();
            drone.sendCmd();
        }



        //esce dalla bootstrap mode(navdata ridotti) ed entra in demo mode(=navdata al completo)
        //viene chiamata all'inizio del programma da timer.
        private void exitBootstrapEnterDemoMode()
        {
            drone.navdataDemoModeOn();
            drone.sendCmd();
        }


        //funzione che periodicamente aggiorna legge i nav data inviati dal drone
        //e li inserisce nelle strutture dati della classe NavData
        //chiamata da timer2.
        private void navdataPeriodicUpdate() {

            //legge navdata dal socket
            //N.B: receiveNavdata() è blocking!!
            if (!nav.receiveNavdata()) {
                status.Text = "NAVDATA non ricevuti!";
                return;
            }

            //aggiorna i dati
            nav.updateNavigationData();


            //crea la stringa di testo coi dati da visualizzare
            String log = "Navigation Data:" + Environment.NewLine + Environment.NewLine;
            log += "Control state: " + nav.getControlStatus() + Environment.NewLine;
            log += "Battery: " + nav.getBatteryLevel() + Environment.NewLine;
            log += "Pitch: " + nav.getPitch() + Environment.NewLine;
            log += "Roll: " + nav.getRoll() + Environment.NewLine;
            log += "Yaw(ang. speed): " + nav.getYaw() + Environment.NewLine;
            log += "Altitude: " + nav.getAltitude() + Environment.NewLine;
            log += "Velocity X: " + nav.getVX() + Environment.NewLine;
            log += "Velocity Y: " + nav.getVY() + Environment.NewLine;
            //log += "Velocity Z: " + nav.getVZ() + Environment.NewLine;


            //visualizza nella textbox la stringa prodotta
            setNavdataBoxText(log);
        }



        //metodo delegato usato per reinvocare la funzione setNavDataBoxText all'interno del thread principale
        //nel caso venisse chiamata nel thread secondario(timer).
        private delegate void EventArgsDelegate(String txt);

        //scrive  a schermo i valori
        private void setNavdataBoxText(String txt) {

            //per via dei timeout che chiamano periodicamente questa funzionela funzione navDataPeriodicUpdate()(che chima qst funz.)
            //in un thread differente da quello del Form, non posso modificare il valore della textbox navdataBox in maniera thread safe.

            //controllo se sono nel thread che ha creato navdataBox(thread principale)
            //se non ctrl. che navdataBox esista alla chiusura del form lancia un eccezione.
            if (this.InvokeRequired && !this.IsDisposed )
            {
                try
                {
                    //sono nel thread sbagliato, chiamo quello giusto
                    this.Invoke(new EventArgsDelegate(setNavdataBoxText), txt);
                }
                catch (System.ObjectDisposedException oe)
                {
                    Console.WriteLine("Eccezione ObjectDisposedException: " + oe.ToString());
                }

            }
            else
            {

                //Ora sono in quello giusto, posso impostare il valore del testo
                //navdataBox.Text = txt; 
                

                //aggiorna le label, se sottyo 30% rosso
                if (nav.getBatteryLevel() <= 30)
                {
                    batteryLabel.ForeColor = System.Drawing.Color.Red;
                }
                else {
                    batteryLabel.ForeColor = System.Drawing.Color.Black;
                }
                
                batteryLabel.Text = "Batteria: " + nav.getBatteryLevel() + "%";

                pitchLabel.Text = "Pitch: " + nav.getPitch();
                rollLabel.Text = "Roll: " + nav.getRoll();
                yawLabel.Text = "Yaw: " + nav.getYaw();

                altitudeLabel.Text = "Altitudine: " + nav.getAltitude();

                vxLabel.Text = "Velocity X: " + nav.getVX();
                vyLabel.Text = "Velocity Y: " + nav.getVY();


            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            maxHLabel.Text = trackBar1.Value.ToString(); //scrivo il valore selez. sulla label

            drone.setMaxAltitude(trackBar1.Value);
            if( drone.isConnectedToDrone() )
                drone.sendCmd();

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            int value0_100 = trackBar2.Value;
            float value0_1 = (float)value0_100 / (float)100.0;

            drone.setSpeed( value0_1 );
            drone.setRotationSpeed(value0_1 * 2);

            //scrivbo il val. selez. sulla label
            speedLabel.Text = value0_1.ToString();

            status.Text = "v=" + value0_1;

        }

        //togliere???...
        private void v(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }



        //update data button
        /*private void button4_Click(object sender, EventArgs e)
        {
            //non aggiorna se non sono presenti dati nuovi inviati dal drone
            if (!nav.isNavdataAvailable())
                return;


            //legge i dati
            NavData.NavigationDataStruct navData;
            nav.updateNavigationData();


            String log = "Navigation Data:" + Environment.NewLine + Environment.NewLine;
            log += "Control state: " + nav.getControlStatus() + Environment.NewLine;
            log += "Battery: " + nav.getBatteryLevel() + Environment.NewLine;
            log += "Pitch: " + nav.getPitch() + Environment.NewLine;
            log += "Roll: " + nav.getRoll() + Environment.NewLine;
            log += "Yaw(ang. speed): " + nav.getYaw() + Environment.NewLine;
            log += "Altitude: " + nav.getAltitude() + Environment.NewLine;
            log += "Velocity X: " + nav.getVX() + Environment.NewLine;
            log += "Velocity Y: " + nav.getVY()  +Environment.NewLine;
            log += "Velocity Z: " + nav.getVZ()  + Environment.NewLine;


            //visualizza a schermo
            navdataBox.Text = log;


        }*/



    }
}
