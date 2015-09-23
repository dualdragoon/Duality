using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace Duality
{
    public partial class EmailForm : Form
    {
        string details;
        public EmailForm(bool internetUp, string errorDetails)
        {
            InitializeComponent();
            if (internetUp)
            {
                internetDownLabel.Visible = false;
                internetDownButton.Visible = false;
                internetUpLabel.Visible = true;
                internetUpYes.Visible = true;
                internetUpNo.Visible = true;
            }
            else
            {
                internetDownLabel.Visible = true;
                internetDownButton.Visible = true;
                internetUpLabel.Visible = false;
                internetUpYes.Visible = false;
                internetUpNo.Visible = false;
            }
            details = errorDetails;
        }

        private void internetDownButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void internetUpNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void internetUpYes_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage("Duality@DualityEngine.org", "hosleraaron06@gmail.com");
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("DualityErrorEmail@gmail.com", "treakle124");
            client.EnableSsl = true;
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            mail.Subject = "Error details.";
            mail.Body = details;
            client.Send(mail);
            Close();
        }
    }
}
