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
            this.internetDownLabel = new System.Windows.Forms.Label();
            this.internetDownButton = new System.Windows.Forms.Button();
            this.internetUpLabel = new System.Windows.Forms.Label();
            this.internetUpYes = new System.Windows.Forms.Button();
            this.internetUpNo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // internetDownLabel
            // 
            this.internetDownLabel.AutoSize = true;
            this.internetDownLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.internetDownLabel.Location = new System.Drawing.Point(60, 70);
            this.internetDownLabel.Name = "internetDownLabel";
            this.internetDownLabel.Size = new System.Drawing.Size(234, 40);
            this.internetDownLabel.TabIndex = 0;
            this.internetDownLabel.Text = "Internet connection unavailable.\r\n      Cannot send error data.";
            this.internetDownLabel.Visible = false;
            // 
            // internetDownButton
            // 
            this.internetDownButton.Location = new System.Drawing.Point(135, 142);
            this.internetDownButton.Name = "internetDownButton";
            this.internetDownButton.Size = new System.Drawing.Size(75, 23);
            this.internetDownButton.TabIndex = 1;
            this.internetDownButton.Text = "Close";
            this.internetDownButton.UseVisualStyleBackColor = true;
            this.internetDownButton.Visible = false;
            this.internetDownButton.Click += new System.EventHandler(this.internetDownButton_Click);
            // 
            // internetUpLabel
            // 
            this.internetUpLabel.AutoSize = true;
            this.internetUpLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.internetUpLabel.Location = new System.Drawing.Point(105, 70);
            this.internetUpLabel.Name = "internetUpLabel";
            this.internetUpLabel.Size = new System.Drawing.Size(139, 40);
            this.internetUpLabel.TabIndex = 2;
            this.internetUpLabel.Text = "   Error occured.\r\nSend error report?";
            this.internetUpLabel.Visible = false;
            // 
            // internetUpYes
            // 
            this.internetUpYes.Location = new System.Drawing.Point(64, 176);
            this.internetUpYes.Name = "internetUpYes";
            this.internetUpYes.Size = new System.Drawing.Size(75, 23);
            this.internetUpYes.TabIndex = 3;
            this.internetUpYes.Text = "Yes";
            this.internetUpYes.UseVisualStyleBackColor = true;
            this.internetUpYes.Visible = false;
            this.internetUpYes.Click += new System.EventHandler(this.internetUpYes_Click);
            // 
            // internetUpNo
            // 
            this.internetUpNo.Location = new System.Drawing.Point(219, 176);
            this.internetUpNo.Name = "internetUpNo";
            this.internetUpNo.Size = new System.Drawing.Size(75, 23);
            this.internetUpNo.TabIndex = 4;
            this.internetUpNo.Text = "No";
            this.internetUpNo.UseVisualStyleBackColor = true;
            this.internetUpNo.Visible = false;
            this.internetUpNo.Click += new System.EventHandler(this.internetUpNo_Click);
            // 
            // EmailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 245);
            this.Controls.Add(this.internetUpNo);
            this.Controls.Add(this.internetUpYes);
            this.Controls.Add(this.internetUpLabel);
            this.Controls.Add(this.internetDownButton);
            this.Controls.Add(this.internetDownLabel);
            this.Name = "EmailForm";
            this.Text = "EmailForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label internetDownLabel;
        private System.Windows.Forms.Button internetDownButton;
        private System.Windows.Forms.Label internetUpLabel;
        private System.Windows.Forms.Button internetUpYes;
        private System.Windows.Forms.Button internetUpNo;
    }
}