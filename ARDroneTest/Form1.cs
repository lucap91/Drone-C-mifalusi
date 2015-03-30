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

                    Thread.Sleep(3000); // you need to wait/check the process started, then...


                    // child, new parent
                    // make 'this' the parent of ffmpeg (presuming you are in scope of a Form or Control)
                    SetParent(ffplay.MainWindowHandle, this.Handle);


                    // window, x, y, width, height, repaint
                    // move the ffplayer window to the top-left corner and set the size to 320x280
                   MoveWindow(ffplay.MainWindowHandle, 713, 314, 320, 280, true);
                   //System.Threading.Timer timer = new System.Threading.Timer(_ => setParentWindowsCallback(), null, 0, 2000); //every 10 seconds
                   
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

        //chiama la funziona WINAPI SetParentWindows per inserire la finestra di ffplay.exe dentro al form
        private void setParentWindowsCallback() {
            if (ffplay != null)
            {
                SetParent(ffplay.MainWindowHandle, this.Handle);
                MoveWindow(ffplay.MainWindowHandle, 0, 0, 320, 280, true);
            }
        }

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
            //drone.sendVideoStreamWakeup();
            //showVideo();


        }

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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //se chiudo ffplay e poi il form ho un eccezione NullPointer
            if ( ffplay != null && !ffplay.HasExited ) {
                ffplay.CloseMainWindow();
                ffplay.Close();
            }
        }

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
                drone.hover();
            }

            status.Text = "Comando inviato: " + drone.getCmd();
            drone.sendCmd();
            
        }

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

        private void playAnimation_Click(object sender, EventArgs e)
        {
            if (animationDrop.SelectedIndex > 0 && animationDrop.SelectedIndex < 19) {
                //drone.playAnimation(animationDrop.SelectedIndex);
                status.Text = "comando non supportato";
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void playLEDAnim_Click(object sender, EventArgs e)
        {
            if (ledAnimationDrop.SelectedIndex > 0 && ledAnimationDrop.SelectedIndex < 13)
            {
                //drone.playLedAnimation( ledAnimationDrop.SelectedIndex, (float)0.5, 2 );
                status.Text = "comando non supportato";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            status.Text = drone.ReceiveData().ToString();
        } //keyDown


    }
}
