using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;

namespace Core.Common.Utilities
{
    public class ContactModel
    {
        public string SendTo { get; set; }
        public string MaXacNhan { get; set; }
        public string HoTen { get; set; }
        public string TenDangNhap { get; set; }
        public List<string> Attachment { get; set; }
    }
   
    public static class SendSMSEmail
    {
        #region Gui EMAIL Nguoi Dung

        public static bool SendDangKyEmail(ContactModel modelmail) //tempalte xml,System.Web.Hosting.HostingEnvironment.MapPath("~/" + SmsTemplateDir + "/BaoCaoThongKe.xml");
        {
            try
            {
                string PathTemplate = HttpContext.Current.Server.MapPath("~/Uploads/Template/TemplateDangKy.txt");
                string Subject = "Kích hoạt tài khoản ";
                if (modelmail != null && !string.IsNullOrEmpty(modelmail.SendTo) && !string.IsNullOrEmpty(PathTemplate))
                {
                    if (!System.IO.File.Exists(PathTemplate))
                    {
                        // Không tìm thấy template
                        return false;
                    }

                    StreamReader doc = System.IO.File.OpenText(PathTemplate);
                    string content = doc.ReadToEnd();
                    if (!string.IsNullOrEmpty(content))
                    {
                        content = content
                                   .Replace("[MaXacNhan]", modelmail.MaXacNhan)
                                   .Replace("[Email]", modelmail.SendTo);
                    }
                    Email objEmail = new Email();
                    objEmail.receivers = modelmail.SendTo;
                    objEmail.subject = Subject;
                    objEmail.content = content;
                    Thread email = new Thread(objEmail.Send);
                    email.Start();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool SendKichHoatEmail(ContactModel modelmail) //tempalte xml,System.Web.Hosting.HostingEnvironment.MapPath("~/" + SmsTemplateDir + "/BaoCaoThongKe.xml");
        {
            try
            {
                string PathTemplate = HttpContext.Current.Server.MapPath("~/Uploads/Template/TemplateKichHoat.txt");
                string Subject = "Kích hoạt tài khoản Cổng thông tin Đông Triều";
                if (modelmail != null && !string.IsNullOrEmpty(modelmail.SendTo) && !string.IsNullOrEmpty(PathTemplate))
                {
                    if (!System.IO.File.Exists(PathTemplate))
                    {
                        // Không tìm thấy template
                        return false;
                    }

                    StreamReader doc = System.IO.File.OpenText(PathTemplate);
                    string content = doc.ReadToEnd();
                    if (!string.IsNullOrEmpty(content))
                    {
                        content = content
                                   .Replace("[MaXacNhan]", modelmail.MaXacNhan)
                                   .Replace("[Email]", modelmail.SendTo);
                    }
                    Email objEmail = new Email();
                    objEmail.receivers = modelmail.SendTo;
                    objEmail.subject = Subject;
                    objEmail.content = content;
                    Thread email = new Thread(objEmail.Send);
                    email.Start();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool SendLayLaiMK(ContactModel modelmail) //tempalte xml,System.Web.Hosting.HostingEnvironment.MapPath("~/" + SmsTemplateDir + "/BaoCaoThongKe.xml");
        {
            try
            {
                string PathTemplate = HttpContext.Current.Server.MapPath("~/Uploads/Template/TemplateLayLaiMK.txt");
                string Subject = "Lấy lại mật khẩu Cổng thông tin Đông Triều";
                if (modelmail != null && !string.IsNullOrEmpty(modelmail.SendTo) && !string.IsNullOrEmpty(PathTemplate))
                {
                    if (!System.IO.File.Exists(PathTemplate))
                    {
                        // Không tìm thấy template
                        return false;
                    }

                    StreamReader doc = System.IO.File.OpenText(PathTemplate);
                    string content = doc.ReadToEnd();
                    if (!string.IsNullOrEmpty(content))
                    {
                        content = content
                                   .Replace("[MaXacNhan]", modelmail.MaXacNhan)
                                   .Replace("[Email]", modelmail.SendTo);
                    }
                    Email objEmail = new Email();
                    objEmail.receivers = modelmail.SendTo;
                    objEmail.subject = Subject;
                    objEmail.content = content;
                    Thread email = new Thread(objEmail.Send);
                    email.Start();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

    }
}
