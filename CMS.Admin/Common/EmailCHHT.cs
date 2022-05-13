using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Business.Entities.Domain;
using System.Net;
using System.Net.Configuration;
using System.Configuration;
using System.Net.Mail;
using log4net;
using Module.Framework;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mime;
using System.Collections.Generic;

namespace CMS.Admin.Common
{
    public class EmailCHHT
    {
        public string receivers { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
        public string host { get; set; }
        public int port { get; set; }
        public string sender { get; set; }
        public string senderPassword { get; set; }
        public List<string> attachment { get; set; }
        public EmailCHHT(string _receivers, string _subject, string _content, List<string> _attachment)
        {
            receivers = _receivers;
            subject = _subject;
            content = _content;
            attachment = _attachment;
            var cauHinh = new CauHinhHeThongAll();
            using (var _DungChungSrv = new DungChungServiceClient())
            {
                var temp = _DungChungSrv.GetAllCauHinh();
                if (temp != null && temp.Data != null && temp.Data.resultObject != null)
                {
                    cauHinh = temp.Data.resultObject;
                }
            }
            var CauHinhMail = cauHinh.cauhinhmail;
            if(CauHinhMail!=null)
            {
                sender = CauHinhMail.Email;
                host = CauHinhMail.Host;
                port = int.Parse(CauHinhMail.Port);
                senderPassword = CauHinhMail.MatKhau;
            }    
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
                client.UseDefaultCredentials = true;
                client.Credentials = cred;

                if (attachment != null)
                {
                    foreach (var attachmentFilename in attachment)
                    {
                        if(File.Exists(attachmentFilename))
                        {
                            Attachment attachment = new Attachment(attachmentFilename, MediaTypeNames.Application.Octet);
                            ContentDisposition disposition = attachment.ContentDisposition;
                            disposition.CreationDate = File.GetCreationTime(attachmentFilename);
                            disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename);
                            disposition.ReadDate = File.GetLastAccessTime(attachmentFilename);
                            disposition.FileName = Path.GetFileName(attachmentFilename);
                            disposition.Size = new FileInfo(attachmentFilename).Length;
                            disposition.DispositionType = DispositionTypeNames.Attachment;
                            objMailMessage.Attachments.Add(attachment);
                        }
                    }
                }

                ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object s, X509Certificate certificate, X509Chain chain,
                    SslPolicyErrors sslPolicyErrors) { return true; };
                client.Send(objMailMessage);
                
            }
            catch (System.Exception ex)
            {
                return;
            }

        }
    }
}