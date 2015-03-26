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
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // connettiButton
            // 
            this.connettiButton.Location = new System.Drawing.Point(18, 18);
            this.connettiButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.connettiButton.Name = "connettiButton";
            this.connettiButton.Size = new System.Drawing.Size(112, 35);
            this.connettiButton.TabIndex = 0;
            this.connettiButton.Text = "CONNETTI";
            this.connettiButton.UseVisualStyleBackColor = true;
            this.connettiButton.Click += new System.EventHandler(this.connettiButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip.Location = new System.Drawing.Point(0, 533);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip.Size = new System.Drawing.Size(1066, 30);
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
            this.takeoffButton.Location = new System.Drawing.Point(18, 102);
            this.takeoffButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.takeoffButton.Name = "takeoffButton";
            this.takeoffButton.Size = new System.Drawing.Size(112, 35);
            this.takeoffButton.TabIndex = 2;
            this.takeoffButton.Text = "Decollo";
            this.takeoffButton.UseVisualStyleBackColor = true;
            this.takeoffButton.Click += new System.EventHandler(this.takeoffButton_Click);
            // 
            // landButton
            // 
            this.landButton.Location = new System.Drawing.Point(18, 162);
            this.landButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.landButton.Name = "landButton";
            this.landButton.Size = new System.Drawing.Size(112, 35);
            this.landButton.TabIndex = 3;
            this.landButton.Text = "Atterra";
            this.landButton.UseVisualStyleBackColor = true;
            this.landButton.Click += new System.EventHandler(this.landButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveUpButton.Location = new System.Drawing.Point(420, 240);
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
            this.moveDownButton.Location = new System.Drawing.Point(420, 362);
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
            this.buttonMoveRight.Location = new System.Drawing.Point(489, 314);
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
            this.buttonMoveLeft.Location = new System.Drawing.Point(324, 314);
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
            this.buttonGoUp.Location = new System.Drawing.Point(324, 40);
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
            this.buttonGoDown.Location = new System.Drawing.Point(324, 118);
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
            this.buttonCalibra.Location = new System.Drawing.Point(18, 277);
            this.buttonCalibra.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCalibra.Name = "buttonCalibra";
            this.buttonCalibra.Size = new System.Drawing.Size(112, 35);
            this.buttonCalibra.TabIndex = 10;
            this.buttonCalibra.Text = "Calibra";
            this.buttonCalibra.UseVisualStyleBackColor = true;
            this.buttonCalibra.Click += new System.EventHandler(this.buttonCalibra_Click);
            // 
            // rotateLeft
            // 
            this.rotateLeft.Location = new System.Drawing.Point(713, 144);
            this.rotateLeft.Name = "rotateLeft";
            this.rotateLeft.Size = new System.Drawing.Size(96, 41);
            this.rotateLeft.TabIndex = 11;
            this.rotateLeft.Text = "LR";
            this.rotateLeft.UseVisualStyleBackColor = true;
            this.rotateLeft.Click += new System.EventHandler(this.rotateLeft_Click);
            // 
            // rotateRight
            // 
            this.rotateRight.Location = new System.Drawing.Point(825, 144);
            this.rotateRight.Name = "rotateRight";
            this.rotateRight.Size = new System.Drawing.Size(99, 40);
            this.rotateRight.TabIndex = 12;
            this.rotateRight.Text = "RR";
            this.rotateRight.UseVisualStyleBackColor = true;
            this.rotateRight.Click += new System.EventHandler(this.rotateRight_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 563);
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
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "mifalusi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
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
    }
}

