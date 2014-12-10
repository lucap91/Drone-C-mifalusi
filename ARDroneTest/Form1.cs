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


        //oggetto drone
        private static ARDrone drone;



        public Form1()
        {
            InitializeComponent();
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
                 //showVideo();
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
                    Arguments = "tcp://192.168.1.1:5555",
                    // hides the command window
                    CreateNoWindow = true,
                    // redirect input, output, and error streams..
                    RedirectStandardError = false,
                    RedirectStandardOutput = false,
                    UseShellExecute = false
                }
            };

            ffplay.EnableRaisingEvents = true;
            ffplay.OutputDataReceived += (o, e) => Debug.WriteLine(e.Data ?? "NULL", "ffplay");
            ffplay.ErrorDataReceived += (o, e) => Debug.WriteLine(e.Data ?? "NULL", "ffplay");
            ffplay.Exited += (o, e) => Debug.WriteLine("Exited", "ffplay");
            ffplay.Start();

            //ShowWindow(ffplay.MainWindowHandle, 10);

           /* ThreadStart ths = new ThreadStart(() => ffplay.Start());
            Thread th = new Thread(ths);
            th.Start();*/
            //video.StartPosition = FormStartPosition.CenterParent;
            //video.ShowDialog(this);
            
            
            //Thread.Sleep(4000); // you need to wait/check the process started, then...
            while (!ffplay.Responding) { Thread.Sleep(2); }

            // child, new parent
            // make 'this' the parent of ffmpeg (presuming you are in scope of a Form or Control)
            SetParent(ffplay.MainWindowHandle, this.Handle);
            //ShowWindow(ffplay.MainWindowHandle, this.Handle);

            // window, x, y, width, height, repaint
            // move the ffplayer window to the top-left corner and set the size to 320x280
            MoveWindow(ffplay.MainWindowHandle, 0, 0, 320, 280, true);
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
                status.Text = "Decollo effettuato.";
            }

            showVideo();


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
                status.Text = "Atterraggio.";
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
                status.Text = "Avanti";
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
                status.Text = "Indietro";
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
                status.Text = "Sinistra";
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
                status.Text = "Destra";
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
                status.Text = "Su";
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
                status.Text = "Giù";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ffplay != null) {
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


    }
}
