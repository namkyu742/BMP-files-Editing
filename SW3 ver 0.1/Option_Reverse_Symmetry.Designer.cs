namespace SW3_ver_0._1
{
    partial class Option_Reverse_Symmetry
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button_Accept = new System.Windows.Forms.Button();
            this.button_Cancle = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 93);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "대칭 반전";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(17, 64);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(103, 16);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "상하 좌우 반전";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Hot_KeyPress);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(17, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(75, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "좌우 반전";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Hot_KeyPress);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(17, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(75, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "상하 반전";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Hot_KeyPress);
            // 
            // button_Accept
            // 
            this.button_Accept.Location = new System.Drawing.Point(12, 111);
            this.button_Accept.Name = "button_Accept";
            this.button_Accept.Size = new System.Drawing.Size(76, 26);
            this.button_Accept.TabIndex = 1;
            this.button_Accept.Text = "적용";
            this.button_Accept.UseVisualStyleBackColor = true;
            this.button_Accept.Click += new System.EventHandler(this.Button_Accept_Click);
            // 
            // button_Cancle
            // 
            this.button_Cancle.Location = new System.Drawing.Point(94, 111);
            this.button_Cancle.Name = "button_Cancle";
            this.button_Cancle.Size = new System.Drawing.Size(76, 26);
            this.button_Cancle.TabIndex = 2;
            this.button_Cancle.Text = "취소";
            this.button_Cancle.UseVisualStyleBackColor = true;
            this.button_Cancle.Click += new System.EventHandler(this.Button_Cancle_Click);
            // 
            // Option_Reverse_Symmetry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 145);
            this.Controls.Add(this.button_Cancle);
            this.Controls.Add(this.button_Accept);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Option_Reverse_Symmetry";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button button_Accept;
        private System.Windows.Forms.Button button_Cancle;
    }
}