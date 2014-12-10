﻿namespace ARDroneTest
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
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // connettiButton
            // 
            this.connettiButton.Location = new System.Drawing.Point(12, 12);
            this.connettiButton.Name = "connettiButton";
            this.connettiButton.Size = new System.Drawing.Size(75, 23);
            this.connettiButton.TabIndex = 0;
            this.connettiButton.Text = "CONNETTI";
            this.connettiButton.UseVisualStyleBackColor = true;
            this.connettiButton.Click += new System.EventHandler(this.connettiButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip.Location = new System.Drawing.Point(0, 344);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(711, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(119, 17);
            this.status.Text = "Drone non connesso.";
            // 
            // takeoffButton
            // 
            this.takeoffButton.Location = new System.Drawing.Point(12, 66);
            this.takeoffButton.Name = "takeoffButton";
            this.takeoffButton.Size = new System.Drawing.Size(75, 23);
            this.takeoffButton.TabIndex = 2;
            this.takeoffButton.Text = "Decollo";
            this.takeoffButton.UseVisualStyleBackColor = true;
            this.takeoffButton.Click += new System.EventHandler(this.takeoffButton_Click);
            // 
            // landButton
            // 
            this.landButton.Location = new System.Drawing.Point(12, 105);
            this.landButton.Name = "landButton";
            this.landButton.Size = new System.Drawing.Size(75, 23);
            this.landButton.TabIndex = 3;
            this.landButton.Text = "Atterra";
            this.landButton.UseVisualStyleBackColor = true;
            this.landButton.Click += new System.EventHandler(this.landButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveUpButton.Location = new System.Drawing.Point(280, 156);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(40, 58);
            this.moveUpButton.TabIndex = 4;
            this.moveUpButton.Text = "˄";
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveDownButton.Location = new System.Drawing.Point(280, 235);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(40, 58);
            this.moveDownButton.TabIndex = 5;
            this.moveDownButton.Text = "˅";
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // buttonMoveRight
            // 
            this.buttonMoveRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMoveRight.Location = new System.Drawing.Point(326, 204);
            this.buttonMoveRight.Name = "buttonMoveRight";
            this.buttonMoveRight.Size = new System.Drawing.Size(58, 40);
            this.buttonMoveRight.TabIndex = 6;
            this.buttonMoveRight.Text = "˃";
            this.buttonMoveRight.UseVisualStyleBackColor = true;
            this.buttonMoveRight.Click += new System.EventHandler(this.buttonMoveRight_Click);
            // 
            // buttonMoveLeft
            // 
            this.buttonMoveLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMoveLeft.Location = new System.Drawing.Point(216, 204);
            this.buttonMoveLeft.Name = "buttonMoveLeft";
            this.buttonMoveLeft.Size = new System.Drawing.Size(58, 40);
            this.buttonMoveLeft.TabIndex = 7;
            this.buttonMoveLeft.Text = "˂";
            this.buttonMoveLeft.UseVisualStyleBackColor = true;
            this.buttonMoveLeft.Click += new System.EventHandler(this.buttonMoveLeft_Click);
            // 
            // buttonGoUp
            // 
            this.buttonGoUp.Location = new System.Drawing.Point(216, 26);
            this.buttonGoUp.Name = "buttonGoUp";
            this.buttonGoUp.Size = new System.Drawing.Size(168, 34);
            this.buttonGoUp.TabIndex = 8;
            this.buttonGoUp.Text = "UP";
            this.buttonGoUp.UseVisualStyleBackColor = true;
            this.buttonGoUp.Click += new System.EventHandler(this.buttonGoUp_Click);
            // 
            // buttonGoDown
            // 
            this.buttonGoDown.Location = new System.Drawing.Point(216, 77);
            this.buttonGoDown.Name = "buttonGoDown";
            this.buttonGoDown.Size = new System.Drawing.Size(168, 34);
            this.buttonGoDown.TabIndex = 9;
            this.buttonGoDown.Text = "DOWN";
            this.buttonGoDown.UseVisualStyleBackColor = true;
            this.buttonGoDown.Click += new System.EventHandler(this.buttonGoDown_Click);
            // 
            // buttonCalibra
            // 
            this.buttonCalibra.Location = new System.Drawing.Point(12, 180);
            this.buttonCalibra.Name = "buttonCalibra";
            this.buttonCalibra.Size = new System.Drawing.Size(75, 23);
            this.buttonCalibra.TabIndex = 10;
            this.buttonCalibra.Text = "Calibra";
            this.buttonCalibra.UseVisualStyleBackColor = true;
            this.buttonCalibra.Click += new System.EventHandler(this.buttonCalibra_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 366);
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
            this.Name = "Form1";
            this.Text = "mifalusi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
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
    }
}
