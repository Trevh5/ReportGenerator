namespace Report_Generator
{
    partial class InitializeArchiveForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(62, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1255, 276);
            this.label3.TabIndex = 1;
            this.label3.Text = "WELCOME TO THE REPORT GENERATOR!\r\n\r\nPLEASE SELECT A LOCATION TO \r\nKEEP YOUR ARCHI" +
    "VE\r\n";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(67, 431);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1243, 228);
            this.label4.TabIndex = 16;
            this.label4.Text = "ARCHIVE LOCATION:\r\nClick \"CHOOSE LOCATION\" to specify";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // button10
            // 
            this.button10.Font = new System.Drawing.Font("Rockwell", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.Location = new System.Drawing.Point(472, 737);
            this.button10.MaximumSize = new System.Drawing.Size(1473, 1189);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(433, 116);
            this.button10.TabIndex = 17;
            this.button10.Text = "CHOOSE LOCATION";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Font = new System.Drawing.Font("Rockwell", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button11.Location = new System.Drawing.Point(472, 923);
            this.button11.MaximumSize = new System.Drawing.Size(1473, 1189);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(433, 116);
            this.button11.TabIndex = 18;
            this.button11.Text = "SAVE LOCATION";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // InitializeArchiveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1455, 1142);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(1473, 1189);
            this.MinimumSize = new System.Drawing.Size(1473, 1189);
            this.Name = "InitializeArchiveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Initialize Archive";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
    }
}