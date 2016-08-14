using System;
using System.Windows.Forms;
using Duality.Encrypting;

namespace Duality
{
    public partial class EmailForm : Form
    {
        string details, user = StringCipher.Decrypt("WXl7/1dqjhWN8KOkkb46Fs9J6WAYjXOth8Rj+J1XjRE=", "agfdegfagagsdfgsfdyjtjk,yiu"), pass = StringCipher.Decrypt("xr382tkMPJjFu+l1SBZ3qg==", "kajufhlgiufefbgaiugroirhen9p8gh2ase");
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
            CDO.Message message = new CDO.Message();
            CDO.IConfiguration configuration = message.Configuration;
            ADODB.Fields fields = configuration.Fields;

            ADODB.Field field = fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"];
            field.Value = "smtp.gmail.com";

            field = fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"];
            field.Value = 465;

            field = fields["http://schemas.microsoft.com/cdo/configuration/sendusing"];
            field.Value = CDO.CdoSendUsing.cdoSendUsingPort;

            field = fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"];
            field.Value = CDO.CdoProtocolsAuthentication.cdoBasic;

            field = fields["http://schemas.microsoft.com/cdo/configuration/sendusername"];
            field.Value = user;

            field = fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"];
            field.Value = pass;

            field = fields["http://schemas.microsoft.com/cdo/configuration/smtpusessl"];
            field.Value = "true";

            fields.Update();

            message.From = user;
            message.To = "hosleraaron06@gmail.com";
            message.Subject = "Error details.";
            message.TextBody = details;

            message.Send();

            /*MailMessage mail = new MailMessage("Duality@DualityEngine.org", "hosleraaron06@gmail.com");
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("hosleraaron06@gmail.com", "dragonslayer123");
            client.EnableSsl = true;
            client.Port = 465;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Host = "smtp.gmail.com";
            mail.Subject = "Error details.";
            mail.Body = details;
            client.Send(mail);*/
            Close();
        }
    }
}
