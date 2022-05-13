using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Configuration;
using System.Configuration;
using System.Net.Mail;
using log4net;

namespace Core.Common.Utilities
{

    public class Email
    {
        public string host { get; set; }
        public int port { get; set; }
        public string sender { get; set; }
        public string senderPassword { get; set; }
        public string receivers { get; set; }
        public string cc { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
        protected static readonly ILog _logger = LogManager.GetLogger(typeof(Email));
        public Email()
        {
            SmtpSection objSmtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            host = objSmtpSection.Network.Host;
            port = objSmtpSection.Network.Port;
            sender = objSmtpSection.Network.UserName;
            senderPassword = objSmtpSection.Network.Password;
        }
        private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            //Console.WriteLine(certificate);
            return true;
        }

        public void Send()
        {
            try
            {
                MailMessage objMailMessage = new MailMessage(sender, receivers, subject, content);
                objMailMessage.IsBodyHtml = true;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                SmtpClient client = new SmtpClient();
                client.Host = host;
                client.Port = port;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                NetworkCredential cred = new System.Net.NetworkCredential(sender, senderPassword);
                client.UseDefaultCredentials = false;
                client.Credentials = cred;
                ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object s, X509Certificate certificate, X509Chain chain,
                    SslPolicyErrors sslPolicyErrors) { return true; };
                client.Send(objMailMessage);
            }
            catch (System.Exception ex)
            {
                _logger.Error("error: " + ex.Message + ": " + ex.StackTrace);
                return;

            }

        }
    }
}