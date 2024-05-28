namespace CheckShort_YC4
{
    partial class FormConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
            this.txtRun = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBoxComSerialPortSettings = new System.Windows.Forms.GroupBox();
            this.btnSaveChanged = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cboCOMPort = new System.Windows.Forms.ComboBox();
            this.btnTestSend = new System.Windows.Forms.Button();
            this.groupBoxComSerialPortSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRun
            // 
            this.txtRun.BackColor = System.Drawing.Color.White;
            this.txtRun.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRun.ForeColor = System.Drawing.Color.Black;
            this.txtRun.Location = new System.Drawing.Point(99, 78);
            this.txtRun.Name = "txtRun";
            this.txtRun.Size = new System.Drawing.Size(73, 22);
            this.txtRun.TabIndex = 0;
            this.txtRun.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRun.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(19, 82);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 16);
            this.label13.TabIndex = 1;
            this.label13.Text = "Signal test:";
            this.label13.Visible = false;
            // 
            // groupBoxComSerialPortSettings
            // 
            this.groupBoxComSerialPortSettings.Controls.Add(this.btnSaveChanged);
            this.groupBoxComSerialPortSettings.Controls.Add(this.txtRun);
            this.groupBoxComSerialPortSettings.Controls.Add(this.label8);
            this.groupBoxComSerialPortSettings.Controls.Add(this.cboCOMPort);
            this.groupBoxComSerialPortSettings.Controls.Add(this.btnTestSend);
            this.groupBoxComSerialPortSettings.Controls.Add(this.label13);
            this.groupBoxComSerialPortSettings.Location = new System.Drawing.Point(2, 24);
            this.groupBoxComSerialPortSettings.Name = "groupBoxComSerialPortSettings";
            this.groupBoxComSerialPortSettings.Size = new System.Drawing.Size(358, 130);
            this.groupBoxComSerialPortSettings.TabIndex = 28;
            this.groupBoxComSerialPortSettings.TabStop = false;
            this.groupBoxComSerialPortSettings.Text = "Port";
            // 
            // btnSaveChanged
            // 
            this.btnSaveChanged.BackColor = System.Drawing.Color.Green;
            this.btnSaveChanged.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveChanged.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveChanged.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveChanged.ForeColor = System.Drawing.Color.White;
            this.btnSaveChanged.Image = global::CheckShort_YC4.Properties.Resources.Save;
            this.btnSaveChanged.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveChanged.Location = new System.Drawing.Point(271, 25);
            this.btnSaveChanged.Name = "btnSaveChanged";
            this.btnSaveChanged.Size = new System.Drawing.Size(72, 28);
            this.btnSaveChanged.TabIndex = 23;
            this.btnSaveChanged.Text = "Save";
            this.btnSaveChanged.UseVisualStyleBackColor = false;
            this.btnSaveChanged.Click += new System.EventHandler(this.btnSaveChanged_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(19, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "Port Name:";
            // 
            // cboCOMPort
            // 
            this.cboCOMPort.FormattingEnabled = true;
            this.cboCOMPort.Location = new System.Drawing.Point(99, 26);
            this.cboCOMPort.Name = "cboCOMPort";
            this.cboCOMPort.Size = new System.Drawing.Size(124, 21);
            this.cboCOMPort.TabIndex = 0;
            // 
            // btnTestSend
            // 
            this.btnTestSend.BackColor = System.Drawing.Color.Green;
            this.btnTestSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestSend.ForeColor = System.Drawing.Color.White;
            this.btnTestSend.Image = global::CheckShort_YC4.Properties.Resources.send_receive;
            this.btnTestSend.Location = new System.Drawing.Point(176, 76);
            this.btnTestSend.Name = "btnTestSend";
            this.btnTestSend.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnTestSend.Size = new System.Drawing.Size(47, 25);
            this.btnTestSend.TabIndex = 23;
            this.btnTestSend.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTestSend.UseVisualStyleBackColor = false;
            this.btnTestSend.Visible = false;
            this.btnTestSend.Click += new System.EventHandler(this.btnTestSend_Click);
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(370, 163);
            this.Controls.Add(this.groupBoxComSerialPortSettings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Config";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.groupBoxComSerialPortSettings.ResumeLayout(false);
            this.groupBoxComSerialPortSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSaveChanged;
        private System.Windows.Forms.TextBox txtRun;
        private System.Windows.Forms.Button btnTestSend;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBoxComSerialPortSettings;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboCOMPort;
    }
}
