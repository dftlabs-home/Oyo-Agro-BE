using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Request;

namespace OyoAgro.DataAccess.Layer.Helpers
{
    public static class EmailHelper
    {
        public static bool IsPasswordEmailSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Templates/_PasswordTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[realname]", user.RealName);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[password]", user.UserPassword);
            builder.Replace("[password]", user.UserCompany);
            builder.Replace("[link]", $"");
            builder.Replace("[year]", DateTime.Now.Year.ToString());
            builder.Replace("[reserved]", GlobalConstant.RESERVED);

            NetworkCredential credential = new NetworkCredential
            {
                UserName = GlobalConstant.CREDENTIAL_USERNAME,
                Password = GlobalConstant.CREDENTIAL_PASSWORD
            };

            MailMessage mail = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(GlobalConstant.MAIL_FROM)
            };
            mail.To.Add(user.UserEmail);
            mail.Subject = "Password Notification";
            mail.Body = builder.ToString();

            SmtpClient smtp = new SmtpClient
            {
                Host = GlobalConstant.SMTP_HOST,
                UseDefaultCredentials = false,
                Credentials = credential,
                Port = GlobalConstant.SMTP_PORT,
                EnableSsl = GlobalConstant.SMTP_SSL
            };

            try
            {
                smtp.Send(mail);
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
