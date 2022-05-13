using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;
namespace CMS.Admin.Common
{
    public class ModelSendMailCHHT
    {
        public string SendTo { get; set; }
        public string TenChuThe { get; set; }
        public string HoTen { get; set; }
        public string MaCoSo { get; set; }
        public string Pass { get; set; }
        public string NoiDung { get; set; }
        public List<string> Attachment { get; set; }
    }

    public static class SendSMSEmailCHHT
    {

        #region sendmail tao tai khoan co so
        public static void SendTaoTaiKhoanCoso(ModelSendMailCHHT modelmail) //tempalte xml,System.Web.Hosting.HostingEnvironment.MapPath("~/" + SmsTemplateDir + "/BaoCaoThongKe.xml");
        {
            try
            {
                string PathTemplate = HttpContext.Current.Server.MapPath("~/Uploads/Template/TemplateTaoTaiKhoan.txt");
                string Subject = "Khởi tạo tài khoản cơ sở";
                if (modelmail != null && !string.IsNullOrEmpty(modelmail.SendTo) && !string.IsNullOrEmpty(PathTemplate))
                {
                    if (!System.IO.File.Exists(PathTemplate))
                    {
                        // Không tìm thấy template
                        return;
                    }
                    StreamReader doc = System.IO.File.OpenText(PathTemplate);
                    string content = doc.ReadToEnd();
                    if (!string.IsNullOrEmpty(content))
                    {
                        content = content
                                   .Replace("[TenCoSo]", modelmail.TenChuThe)
                                   .Replace("[MaCoSo]", modelmail.MaCoSo)
                                   .Replace("[Pass]", modelmail.Pass);
                    }
                    EmailCHHT objEmail = new EmailCHHT(modelmail.SendTo, Subject, content, null);
                    Thread email = new Thread(objEmail.Send);
                    email.Start();
                }
                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }
        #endregion

        #region sendmail so thong bao
        public static void SendMailSoThongBao(ModelSendMailCHHT modelmail) //tempalte xml,System.Web.Hosting.HostingEnvironment.MapPath("~/" + SmsTemplateDir + "/BaoCaoThongKe.xml");
        {
            try
            {
                string PathTemplate = HttpContext.Current.Server.MapPath("~/Uploads/Template/TemplateSoThongBao.txt");
                string Subject = "Thông báo";
                if (modelmail != null && !string.IsNullOrEmpty(modelmail.SendTo) && !string.IsNullOrEmpty(PathTemplate))
                {
                    if (!System.IO.File.Exists(PathTemplate))
                    {
                        // Không tìm thấy template
                        return;
                    }
                    StreamReader doc = System.IO.File.OpenText(PathTemplate);
                    string content = doc.ReadToEnd();
                    if (!string.IsNullOrEmpty(content))
                    {
                        content = content
                                   .Replace("[TenCoSo]", modelmail.TenChuThe)
                                   .Replace("[MaCoSo]", modelmail.MaCoSo)
                                   .Replace("[NoiDung]", modelmail.NoiDung);
                    }
                    EmailCHHT objEmail = new EmailCHHT(modelmail.SendTo, Subject, content, modelmail.Attachment);
                    Thread email = new Thread(objEmail.Send);
                    email.Start();
                }
                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }
        #endregion

    }
}
