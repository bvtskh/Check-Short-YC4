namespace CheckShort_YC4
{
    partial class FormTest
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
            this.btnCom2 = new System.Windows.Forms.Button();
            this.btnCom1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnsend2 = new System.Windows.Forms.Button();
            this.btnsend1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnCom2
            // 
            this.btnCom2.Location = new System.Drawing.Point(190, 53);
            this.btnCom2.Name = "btnCom2";
            this.btnCom2.Size = new System.Drawing.Size(75, 23);
            this.btnCom2.TabIndex = 0;
            this.btnCom2.Text = "kết nối";
            this.btnCom2.UseVisualStyleBackColor = true;
            this.btnCom2.Click += new System.EventHandler(this.btnCom2_Click);
            // 
            // btnCom1
            // 
            this.btnCom1.Location = new System.Drawing.Point(190, 13);
            this.btnCom1.Name = "btnCom1";
            this.btnCom1.Size = new System.Drawing.Size(75, 23);
            this.btnCom1.TabIndex = 0;
            this.btnCom1.Text = "kết nối";
            this.btnCom1.UseVisualStyleBackColor = true;
            this.btnCom1.Click += new System.EventHandler(this.btnCom1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(172, 24);
            this.comboBox1.TabIndex = 1;
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(12, 52);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(172, 24);
            this.comboBox2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(290, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Disconnect";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Disconnect";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 210);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(253, 22);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(12, 250);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(253, 22);
            this.textBox2.TabIndex = 3;
            // 
            // btnsend2
            // 
            this.btnsend2.Location = new System.Drawing.Point(276, 249);
            this.btnsend2.Name = "btnsend2";
            this.btnsend2.Size = new System.Drawing.Size(75, 23);
            this.btnsend2.TabIndex = 0;
            this.btnsend2.Text = "send";
            this.btnsend2.UseVisualStyleBackColor = true;
            this.btnsend2.Click += new System.EventHandler(this.btnsend2_Click);
            // 
            // btnsend1
            // 
            this.btnsend1.Location = new System.Drawing.Point(276, 209);
            this.btnsend1.Name = "btnsend1";
            this.btnsend1.Size = new System.Drawing.Size(75, 23);
            this.btnsend1.TabIndex = 0;
            this.btnsend1.Text = "send";
            this.btnsend1.UseVisualStyleBackColor = true;
            this.btnsend1.Click += new System.EventHandler(this.btnsend1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(383, 18);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(405, 420);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnsend1);
            this.Controls.Add(this.btnsend2);
            this.Controls.Add(this.btnCom1);
            this.Controls.Add(this.btnCom2);
            this.Name = "FormTest";
            this.Text = "FormTest";
            this.Load += new System.EventHandler(this.FormTest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCom2;
        private System.Windows.Forms.Button btnCom1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnsend2;
        private System.Windows.Forms.Button btnsend1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}