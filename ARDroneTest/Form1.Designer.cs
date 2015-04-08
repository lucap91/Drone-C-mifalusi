namespace ARDroneTest
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.connettiButton = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.takeoffButton = new System.Windows.Forms.Button();
            this.landButton = new System.Windows.Forms.Button();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.buttonMoveRight = new System.Windows.Forms.Button();
            this.buttonMoveLeft = new System.Windows.Forms.Button();
            this.buttonGoUp = new System.Windows.Forms.Button();
            this.buttonGoDown = new System.Windows.Forms.Button();
            this.buttonCalibra = new System.Windows.Forms.Button();
            this.rotateLeft = new System.Windows.Forms.Button();
            this.rotateRight = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.maxHLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.speedLabel = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.batteryLabel = new System.Windows.Forms.Label();
            this.pitchLabel = new System.Windows.Forms.Label();
            this.rollLabel = new System.Windows.Forms.Label();
            this.yawLabel = new System.Windows.Forms.Label();
            this.altitudeLabel = new System.Windows.Forms.Label();
            this.vxLabel = new System.Windows.Forms.Label();
            this.vyLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // connettiButton
            // 
            this.connettiButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connettiButton.Location = new System.Drawing.Point(18, 18);
            this.connettiButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.connettiButton.Name = "connettiButton";
            this.connettiButton.Size = new System.Drawing.Size(129, 35);
            this.connettiButton.TabIndex = 0;
            this.connettiButton.Text = "CONNETTI";
            this.connettiButton.UseVisualStyleBackColor = true;
            this.connettiButton.Click += new System.EventHandler(this.connettiButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip.Location = new System.Drawing.Point(0, 692);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip.Size = new System.Drawing.Size(1288, 30);
            this.statusStrip.TabIndex = 1;
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(181, 25);
            this.status.Text = "Drone non connesso.";
            // 
            // takeoffButton
            // 
            this.takeoffButton.Location = new System.Drawing.Point(18, 96);
            this.takeoffButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.takeoffButton.Name = "takeoffButton";
            this.takeoffButton.Size = new System.Drawing.Size(129, 35);
            this.takeoffButton.TabIndex = 2;
            this.takeoffButton.Text = "Decollo";
            this.takeoffButton.UseVisualStyleBackColor = true;
            this.takeoffButton.Click += new System.EventHandler(this.takeoffButton_Click);
            // 
            // landButton
            // 
            this.landButton.Location = new System.Drawing.Point(18, 141);
            this.landButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.landButton.Name = "landButton";
            this.landButton.Size = new System.Drawing.Size(129, 35);
            this.landButton.TabIndex = 3;
            this.landButton.Text = "Atterra";
            this.landButton.UseVisualStyleBackColor = true;
            this.landButton.Click += new System.EventHandler(this.landButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveUpButton.Location = new System.Drawing.Point(537, 245);
            this.moveUpButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(60, 89);
            this.moveUpButton.TabIndex = 4;
            this.moveUpButton.Text = "˄";
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveDownButton.Location = new System.Drawing.Point(537, 367);
            this.moveDownButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(60, 89);
            this.moveDownButton.TabIndex = 5;
            this.moveDownButton.Text = "˅";
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // buttonMoveRight
            // 
            this.buttonMoveRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMoveRight.Location = new System.Drawing.Point(606, 319);
            this.buttonMoveRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonMoveRight.Name = "buttonMoveRight";
            this.buttonMoveRight.Size = new System.Drawing.Size(87, 62);
            this.buttonMoveRight.TabIndex = 6;
            this.buttonMoveRight.Text = "˃";
            this.buttonMoveRight.UseVisualStyleBackColor = true;
            this.buttonMoveRight.Click += new System.EventHandler(this.buttonMoveRight_Click);
            // 
            // buttonMoveLeft
            // 
            this.buttonMoveLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMoveLeft.Location = new System.Drawing.Point(441, 319);
            this.buttonMoveLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonMoveLeft.Name = "buttonMoveLeft";
            this.buttonMoveLeft.Size = new System.Drawing.Size(87, 62);
            this.buttonMoveLeft.TabIndex = 7;
            this.buttonMoveLeft.Text = "˂";
            this.buttonMoveLeft.UseVisualStyleBackColor = true;
            this.buttonMoveLeft.Click += new System.EventHandler(this.buttonMoveLeft_Click);
            // 
            // buttonGoUp
            // 
            this.buttonGoUp.Location = new System.Drawing.Point(441, 160);
            this.buttonGoUp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonGoUp.Name = "buttonGoUp";
            this.buttonGoUp.Size = new System.Drawing.Size(252, 52);
            this.buttonGoUp.TabIndex = 8;
            this.buttonGoUp.Text = "UP";
            this.buttonGoUp.UseVisualStyleBackColor = true;
            this.buttonGoUp.Click += new System.EventHandler(this.buttonGoUp_Click);
            // 
            // buttonGoDown
            // 
            this.buttonGoDown.Location = new System.Drawing.Point(441, 483);
            this.buttonGoDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonGoDown.Name = "buttonGoDown";
            this.buttonGoDown.Size = new System.Drawing.Size(252, 52);
            this.buttonGoDown.TabIndex = 9;
            this.buttonGoDown.Text = "DOWN";
            this.buttonGoDown.UseVisualStyleBackColor = true;
            this.buttonGoDown.Click += new System.EventHandler(this.buttonGoDown_Click);
            // 
            // buttonCalibra
            // 
            this.buttonCalibra.Location = new System.Drawing.Point(18, 240);
            this.buttonCalibra.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCalibra.Name = "buttonCalibra";
            this.buttonCalibra.Size = new System.Drawing.Size(129, 35);
            this.buttonCalibra.TabIndex = 10;
            this.buttonCalibra.Text = "Calibra";
            this.buttonCalibra.UseVisualStyleBackColor = true;
            this.buttonCalibra.Click += new System.EventHandler(this.buttonCalibra_Click);
            // 
            // rotateLeft
            // 
            this.rotateLeft.Location = new System.Drawing.Point(315, 245);
            this.rotateLeft.Name = "rotateLeft";
            this.rotateLeft.Size = new System.Drawing.Size(121, 48);
            this.rotateLeft.TabIndex = 11;
            this.rotateLeft.Text = "LR";
            this.rotateLeft.UseVisualStyleBackColor = true;
            this.rotateLeft.Click += new System.EventHandler(this.rotateLeft_Click);
            // 
            // rotateRight
            // 
            this.rotateRight.Location = new System.Drawing.Point(706, 245);
            this.rotateRight.Name = "rotateRight";
            this.rotateRight.Size = new System.Drawing.Size(121, 48);
            this.rotateRight.TabIndex = 12;
            this.rotateRight.Text = "RR";
            this.rotateRight.UseVisualStyleBackColor = true;
            this.rotateRight.Click += new System.EventHandler(this.rotateRight_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(839, 458);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "Altezza Massima(m)";
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 3;
            this.trackBar1.Location = new System.Drawing.Point(997, 453);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(205, 69);
            this.trackBar1.TabIndex = 20;
            this.trackBar1.Value = 3;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // maxHLabel
            // 
            this.maxHLabel.AutoSize = true;
            this.maxHLabel.Location = new System.Drawing.Point(1086, 491);
            this.maxHLabel.Name = "maxHLabel";
            this.maxHLabel.Size = new System.Drawing.Size(18, 20);
            this.maxHLabel.TabIndex = 21;
            this.maxHLabel.Text = "3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(839, 536);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 20);
            this.label2.TabIndex = 22;
            this.label2.Text = "Velocità Movimento";
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Location = new System.Drawing.Point(1064, 577);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(40, 20);
            this.speedLabel.TabIndex = 23;
            this.speedLabel.Text = "0.25";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(997, 528);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Minimum = 1;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(205, 69);
            this.trackBar2.TabIndex = 24;
            this.trackBar2.Value = 25;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // batteryLabel
            // 
            this.batteryLabel.AutoSize = true;
            this.batteryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.batteryLabel.Location = new System.Drawing.Point(38, 71);
            this.batteryLabel.Name = "batteryLabel";
            this.batteryLabel.Size = new System.Drawing.Size(89, 25);
            this.batteryLabel.TabIndex = 25;
            this.batteryLabel.Text = "Batteria: ";
            // 
            // pitchLabel
            // 
            this.pitchLabel.AutoSize = true;
            this.pitchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pitchLabel.Location = new System.Drawing.Point(38, 117);
            this.pitchLabel.Name = "pitchLabel";
            this.pitchLabel.Size = new System.Drawing.Size(66, 25);
            this.pitchLabel.TabIndex = 26;
            this.pitchLabel.Text = "Pitch: ";
            this.pitchLabel.Click += new System.EventHandler(this.v);
            // 
            // rollLabel
            // 
            this.rollLabel.AutoSize = true;
            this.rollLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rollLabel.Location = new System.Drawing.Point(38, 147);
            this.rollLabel.Name = "rollLabel";
            this.rollLabel.Size = new System.Drawing.Size(55, 25);
            this.rollLabel.TabIndex = 27;
            this.rollLabel.Text = "Roll: ";
            // 
            // yawLabel
            // 
            this.yawLabel.AutoSize = true;
            this.yawLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yawLabel.Location = new System.Drawing.Point(38, 172);
            this.yawLabel.Name = "yawLabel";
            this.yawLabel.Size = new System.Drawing.Size(61, 25);
            this.yawLabel.TabIndex = 28;
            this.yawLabel.Text = "Yaw: ";
            // 
            // altitudeLabel
            // 
            this.altitudeLabel.AutoSize = true;
            this.altitudeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.altitudeLabel.Location = new System.Drawing.Point(38, 197);
            this.altitudeLabel.Name = "altitudeLabel";
            this.altitudeLabel.Size = new System.Drawing.Size(103, 25);
            this.altitudeLabel.TabIndex = 29;
            this.altitudeLabel.Text = "Altitudine: ";
            // 
            // vxLabel
            // 
            this.vxLabel.AutoSize = true;
            this.vxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vxLabel.Location = new System.Drawing.Point(38, 265);
            this.vxLabel.Name = "vxLabel";
            this.vxLabel.Size = new System.Drawing.Size(100, 25);
            this.vxLabel.TabIndex = 30;
            this.vxLabel.Text = "Velocity X";
            this.vxLabel.Click += new System.EventHandler(this.label8_Click);
            // 
            // vyLabel
            // 
            this.vyLabel.AutoSize = true;
            this.vyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vyLabel.Location = new System.Drawing.Point(38, 290);
            this.vyLabel.Name = "vyLabel";
            this.vyLabel.Size = new System.Drawing.Size(94, 25);
            this.vyLabel.TabIndex = 31;
            this.vyLabel.Text = "VelocityY";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.vyLabel);
            this.groupBox1.Controls.Add(this.vxLabel);
            this.groupBox1.Controls.Add(this.batteryLabel);
            this.groupBox1.Controls.Add(this.altitudeLabel);
            this.groupBox1.Controls.Add(this.pitchLabel);
            this.groupBox1.Controls.Add(this.yawLabel);
            this.groupBox1.Controls.Add(this.rollLabel);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(937, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 381);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Telemetria";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1288, 722);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.maxHLabel);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rotateRight);
            this.Controls.Add(this.rotateLeft);
            this.Controls.Add(this.buttonCalibra);
            this.Controls.Add(this.buttonGoDown);
            this.Controls.Add(this.buttonGoUp);
            this.Controls.Add(this.buttonMoveLeft);
            this.Controls.Add(this.buttonMoveRight);
            this.Controls.Add(this.moveDownButton);
            this.Controls.Add(this.moveUpButton);
            this.Controls.Add(this.landButton);
            this.Controls.Add(this.takeoffButton);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.connettiButton);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "mifalusi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connettiButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.Button takeoffButton;
        private System.Windows.Forms.Button landButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button buttonMoveRight;
        private System.Windows.Forms.Button buttonMoveLeft;
        private System.Windows.Forms.Button buttonGoUp;
        private System.Windows.Forms.Button buttonGoDown;
        private System.Windows.Forms.Button buttonCalibra;
        private System.Windows.Forms.Button rotateLeft;
        private System.Windows.Forms.Button rotateRight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label maxHLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label batteryLabel;
        private System.Windows.Forms.Label pitchLabel;
        private System.Windows.Forms.Label rollLabel;
        private System.Windows.Forms.Label yawLabel;
        private System.Windows.Forms.Label altitudeLabel;
        private System.Windows.Forms.Label vxLabel;
        private System.Windows.Forms.Label vyLabel;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

