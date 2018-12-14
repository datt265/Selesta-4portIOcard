namespace RoboticsPOC
{
    partial class RoboticsPOC
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label lblMagazine;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoboticsPOC));
            this.groupBoxSensors = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cassettePresense = new System.Windows.Forms.Label();
            this.cassetteDetectionMethod = new System.Windows.Forms.CheckBox();
            this.lblCasette = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.magazineDetectionMethod = new System.Windows.Forms.ComboBox();
            this.unloaderPresense = new System.Windows.Forms.Label();
            this.unloaderDetectionMethod = new System.Windows.Forms.CheckBox();
            this.lblUnloadBuffer = new System.Windows.Forms.Label();
            this.loaderPresense = new System.Windows.Forms.Label();
            this.loaderDetectionMethod = new System.Windows.Forms.CheckBox();
            this.lblLoadBuffer = new System.Windows.Forms.Label();
            this.groupBoxEMCS = new System.Windows.Forms.GroupBox();
            this.boardConnectionState = new System.Windows.Forms.Label();
            this.messageLog = new System.Windows.Forms.ListBox();
            this.hardwareInitTimer = new System.Windows.Forms.Timer(this.components);
            lblMagazine = new System.Windows.Forms.Label();
            this.groupBoxSensors.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxEMCS.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMagazine
            // 
            lblMagazine.AutoSize = true;
            lblMagazine.Location = new System.Drawing.Point(171, 110);
            lblMagazine.Name = "lblMagazine";
            lblMagazine.Size = new System.Drawing.Size(70, 15);
            lblMagazine.TabIndex = 24;
            lblMagazine.Text = "Magazine";
            // 
            // groupBoxSensors
            // 
            this.groupBoxSensors.Controls.Add(this.groupBox2);
            this.groupBoxSensors.Controls.Add(this.groupBox1);
            this.groupBoxSensors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxSensors.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSensors.Name = "groupBoxSensors";
            this.groupBoxSensors.Size = new System.Drawing.Size(438, 228);
            this.groupBoxSensors.TabIndex = 0;
            this.groupBoxSensors.TabStop = false;
            this.groupBoxSensors.Text = "Sensors";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cassettePresense);
            this.groupBox2.Controls.Add(this.cassetteDetectionMethod);
            this.groupBox2.Controls.Add(this.lblCasette);
            this.groupBox2.Location = new System.Drawing.Point(25, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(398, 56);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            // 
            // cassettePresense
            // 
            this.cassettePresense.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cassettePresense.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cassettePresense.Location = new System.Drawing.Point(11, 24);
            this.cassettePresense.Name = "cassettePresense";
            this.cassettePresense.Size = new System.Drawing.Size(150, 23);
            this.cassettePresense.TabIndex = 23;
            this.cassettePresense.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cassetteDetectionMethod
            // 
            this.cassetteDetectionMethod.AutoSize = true;
            this.cassetteDetectionMethod.Location = new System.Drawing.Point(284, 24);
            this.cassetteDetectionMethod.Name = "cassetteDetectionMethod";
            this.cassetteDetectionMethod.Size = new System.Drawing.Size(77, 19);
            this.cassetteDetectionMethod.TabIndex = 22;
            this.cassetteDetectionMethod.Text = "Inverted";
            this.cassetteDetectionMethod.UseVisualStyleBackColor = true;
            // 
            // lblCasette
            // 
            this.lblCasette.AutoSize = true;
            this.lblCasette.Location = new System.Drawing.Point(167, 25);
            this.lblCasette.Name = "lblCasette";
            this.lblCasette.Size = new System.Drawing.Size(62, 15);
            this.lblCasette.TabIndex = 21;
            this.lblCasette.Text = "Cassette";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.magazineDetectionMethod);
            this.groupBox1.Controls.Add(lblMagazine);
            this.groupBox1.Controls.Add(this.unloaderPresense);
            this.groupBox1.Controls.Add(this.unloaderDetectionMethod);
            this.groupBox1.Controls.Add(this.lblUnloadBuffer);
            this.groupBox1.Controls.Add(this.loaderPresense);
            this.groupBox1.Controls.Add(this.loaderDetectionMethod);
            this.groupBox1.Controls.Add(this.lblLoadBuffer);
            this.groupBox1.Location = new System.Drawing.Point(21, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(402, 140);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output";
            // 
            // magazineDetectionMethod
            // 
            this.magazineDetectionMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.magazineDetectionMethod.FormattingEnabled = true;
            this.magazineDetectionMethod.Location = new System.Drawing.Point(288, 107);
            this.magazineDetectionMethod.Name = "magazineDetectionMethod";
            this.magazineDetectionMethod.Size = new System.Drawing.Size(85, 23);
            this.magazineDetectionMethod.TabIndex = 26;
            // 
            // unloaderPresense
            // 
            this.unloaderPresense.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.unloaderPresense.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unloaderPresense.Location = new System.Drawing.Point(15, 63);
            this.unloaderPresense.Name = "unloaderPresense";
            this.unloaderPresense.Size = new System.Drawing.Size(150, 23);
            this.unloaderPresense.TabIndex = 23;
            this.unloaderPresense.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // unloaderDetectionMethod
            // 
            this.unloaderDetectionMethod.AutoSize = true;
            this.unloaderDetectionMethod.Location = new System.Drawing.Point(288, 63);
            this.unloaderDetectionMethod.Name = "unloaderDetectionMethod";
            this.unloaderDetectionMethod.Size = new System.Drawing.Size(77, 19);
            this.unloaderDetectionMethod.TabIndex = 22;
            this.unloaderDetectionMethod.Text = "Inverted";
            this.unloaderDetectionMethod.UseVisualStyleBackColor = true;
            // 
            // lblUnloadBuffer
            // 
            this.lblUnloadBuffer.AutoSize = true;
            this.lblUnloadBuffer.Location = new System.Drawing.Point(171, 64);
            this.lblUnloadBuffer.Name = "lblUnloadBuffer";
            this.lblUnloadBuffer.Size = new System.Drawing.Size(95, 15);
            this.lblUnloadBuffer.TabIndex = 21;
            this.lblUnloadBuffer.Text = "Unload Buffer";
            // 
            // loaderPresense
            // 
            this.loaderPresense.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.loaderPresense.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loaderPresense.Location = new System.Drawing.Point(15, 26);
            this.loaderPresense.Name = "loaderPresense";
            this.loaderPresense.Size = new System.Drawing.Size(150, 23);
            this.loaderPresense.TabIndex = 20;
            this.loaderPresense.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loaderDetectionMethod
            // 
            this.loaderDetectionMethod.AutoSize = true;
            this.loaderDetectionMethod.Location = new System.Drawing.Point(288, 26);
            this.loaderDetectionMethod.Name = "loaderDetectionMethod";
            this.loaderDetectionMethod.Size = new System.Drawing.Size(77, 19);
            this.loaderDetectionMethod.TabIndex = 19;
            this.loaderDetectionMethod.Text = "Inverted";
            this.loaderDetectionMethod.UseVisualStyleBackColor = true;
            // 
            // lblLoadBuffer
            // 
            this.lblLoadBuffer.AutoSize = true;
            this.lblLoadBuffer.Location = new System.Drawing.Point(171, 27);
            this.lblLoadBuffer.Name = "lblLoadBuffer";
            this.lblLoadBuffer.Size = new System.Drawing.Size(81, 15);
            this.lblLoadBuffer.TabIndex = 18;
            this.lblLoadBuffer.Text = "Load Buffer";
            // 
            // groupBoxEMCS
            // 
            this.groupBoxEMCS.Controls.Add(this.boardConnectionState);
            this.groupBoxEMCS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxEMCS.Location = new System.Drawing.Point(456, 12);
            this.groupBoxEMCS.Name = "groupBoxEMCS";
            this.groupBoxEMCS.Size = new System.Drawing.Size(143, 54);
            this.groupBoxEMCS.TabIndex = 1;
            this.groupBoxEMCS.TabStop = false;
            this.groupBoxEMCS.Text = "Card Status";
            // 
            // boardConnectionState
            // 
            this.boardConnectionState.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.boardConnectionState.Location = new System.Drawing.Point(6, 19);
            this.boardConnectionState.Name = "boardConnectionState";
            this.boardConnectionState.Size = new System.Drawing.Size(131, 22);
            this.boardConnectionState.TabIndex = 3;
            this.boardConnectionState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // messageLog
            // 
            this.messageLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageLog.FormattingEnabled = true;
            this.messageLog.Location = new System.Drawing.Point(15, 259);
            this.messageLog.Name = "messageLog";
            this.messageLog.ScrollAlwaysVisible = true;
            this.messageLog.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.messageLog.Size = new System.Drawing.Size(584, 147);
            this.messageLog.TabIndex = 2;
            // 
            // hardwareInitTimer
            // 
            this.hardwareInitTimer.Tick += new System.EventHandler(this.hardwareInitTimer_Tick);
            // 
            // RoboticsPOC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 418);
            this.Controls.Add(this.messageLog);
            this.Controls.Add(this.groupBoxEMCS);
            this.Controls.Add(this.groupBoxSensors);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(383, 370);
            this.Name = "RoboticsPOC";
            this.Text = "Robotics P.O.C.";
            this.groupBoxSensors.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxEMCS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSensors;
        private System.Windows.Forms.GroupBox groupBoxEMCS;
        private System.Windows.Forms.ListBox messageLog;
        private System.Windows.Forms.Label boardConnectionState;
        private System.Windows.Forms.Timer hardwareInitTimer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label cassettePresense;
        private System.Windows.Forms.CheckBox cassetteDetectionMethod;
        private System.Windows.Forms.Label lblCasette;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox magazineDetectionMethod;
        private System.Windows.Forms.Label unloaderPresense;
        private System.Windows.Forms.CheckBox unloaderDetectionMethod;
        private System.Windows.Forms.Label lblUnloadBuffer;
        private System.Windows.Forms.Label loaderPresense;
        private System.Windows.Forms.CheckBox loaderDetectionMethod;
        private System.Windows.Forms.Label lblLoadBuffer;
    }
}

