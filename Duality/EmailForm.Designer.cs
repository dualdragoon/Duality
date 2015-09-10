namespace Duality
{
    partial class EmailForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.internetDownButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Internet connection unavailable.\r\n      Cannot send error data.";
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // internetDownButton
            // 
            this.internetDownButton.Location = new System.Drawing.Point(135, 138);
            this.internetDownButton.Name = "internetDownButton";
            this.internetDownButton.Size = new System.Drawing.Size(75, 23);
            this.internetDownButton.TabIndex = 1;
            this.internetDownButton.Text = "Close";
            this.internetDownButton.UseVisualStyleBackColor = true;
            // 
            // EmailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 245);
            this.Controls.Add(this.internetDownButton);
            this.Controls.Add(this.label1);
            this.Name = "EmailForm";
            this.Text = "EmailForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button internetDownButton;
    }
}