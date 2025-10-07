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

            try
            {
                var builder = new StringBuilder();

                // Use AppDomain path so it works in IIS publish too
                string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "_PasswordTemplate.cshtml");

                if (!File.Exists(templatePath))
                    throw new FileNotFoundException($"Email template not found: {templatePath}");

                using (StreamReader reader = new StreamReader(templatePath))
                {
                    builder.Append(reader.ReadToEnd());
                }

                builder.Replace("[realname]", user.RealName);
                builder.Replace("[username]", user.UserName);
                builder.Replace("[password]", user.UserPassword);
                builder.Replace("[company]", user.UserCompany);
                builder.Replace("[link]", string.Empty);
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
                    EnableSsl = GlobalConstant.SMTP_SSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 20000
                };

                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                LogErrorToFile(ex);   // ✅ Log error to file
                return false;
            }
        }
        private static void LogErrorToFile(Exception ex)
        {
            try
            {
                Console.WriteLine("==========================================");
                Console.WriteLine($"Time: {DateTime.Now}");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"InnerException: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner Stack: {ex.InnerException.StackTrace}");
                }

                Console.WriteLine("==========================================");
                Console.WriteLine();
            }
            catch
            {
                // avoid recursive crash if logging fails
            }
        }

    }


}
