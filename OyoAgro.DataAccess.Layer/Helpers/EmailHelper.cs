using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Request;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OyoAgro.DataAccess.Layer.Helpers
{
    public static class EmailHelper
    {

        //public static bool IsPasswordEmailSent(MailParameter user, out string message)
        //{
        //    message = string.Empty;

        //    try
        //    {
        //        var builder = new StringBuilder();

        //        // Use AppDomain path so it works in IIS publish too
        //        string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "_PasswordTemplate.cshtml");

        //        if (!File.Exists(templatePath))
        //            throw new FileNotFoundException($"Email template not found: {templatePath}");

        //        using (StreamReader reader = new StreamReader(templatePath))
        //        {
        //            builder.Append(reader.ReadToEnd());
        //        }

        //        builder.Replace("[realname]", user.RealName);
        //        builder.Replace("[username]", user.UserName);
        //        builder.Replace("[password]", user.UserPassword);
        //        builder.Replace("[company]", user.UserCompany);
        //        builder.Replace("[link]", string.Empty);
        //        builder.Replace("[year]", DateTime.Now.Year.ToString());
        //        builder.Replace("[reserved]", GlobalConstant.RESERVED);

        //        NetworkCredential credential = new NetworkCredential
        //        {
        //            UserName = GlobalConstant.CREDENTIAL_USERNAME,
        //            Password = GlobalConstant.CREDENTIAL_PASSWORD
        //        };

        //        MailMessage mail = new MailMessage
        //        {
        //            IsBodyHtml = true,
        //            From = new MailAddress(GlobalConstant.MAIL_FROM)
        //        };
        //        mail.To.Add(user.UserEmail);
        //        mail.Subject = "Password Notification";
        //        mail.Body = builder.ToString();

        //        SmtpClient smtp = new SmtpClient
        //        {
        //            Host = GlobalConstant.SMTP_HOST,
        //            UseDefaultCredentials = false,
        //            Credentials = credential,
        //            Port = GlobalConstant.SMTP_PORT,
        //            EnableSsl = GlobalConstant.SMTP_SSL,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            Timeout = 20000
        //        };

        //        smtp.Send(mail);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //        LogErrorToFile(ex);   // ✅ Log error to file
        //        return false;
        //    }
        //}
        //private static void LogErrorToFile(Exception ex)
        //{
        //    try
        //    {
        //        Console.WriteLine("==========================================");
        //        Console.WriteLine($"Time: {DateTime.Now}");
        //        Console.WriteLine($"Message: {ex.Message}");
        //        Console.WriteLine($"StackTrace: {ex.StackTrace}");

        //        if (ex.InnerException != null)
        //        {
        //            Console.WriteLine($"InnerException: {ex.InnerException.Message}");
        //            Console.WriteLine($"Inner Stack: {ex.InnerException.StackTrace}");
        //        }

        //        Console.WriteLine("==========================================");
        //        Console.WriteLine();
        //    }
        //    catch
        //    {
        //        // avoid recursive crash if logging fails
        //    }
        //}



        //public static bool IsPasswordEmailSent(MailParameter user, out string message)
        //{
        //    message = string.Empty;

        //    try
        //    {
        //        var builder = new StringBuilder();

        //        // Use AppDomain path so it works in Railway too
        //        string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "_PasswordTemplate.cshtml");

        //        if (!File.Exists(templatePath))
        //            throw new FileNotFoundException($"Email template not found: {templatePath}");

        //        using (StreamReader reader = new StreamReader(templatePath))
        //        {
        //            builder.Append(reader.ReadToEnd());
        //        }

        //        builder.Replace("[realname]", user.RealName);
        //        builder.Replace("[username]", user.UserName);
        //        builder.Replace("[password]", user.UserPassword);
        //        builder.Replace("[company]", user.UserCompany);
        //        builder.Replace("[link]", string.Empty);
        //        builder.Replace("[year]", DateTime.Now.Year.ToString());
        //        builder.Replace("[reserved]", GlobalConstant.RESERVED);

        //        // ✅ Load credentials from Railway env variables
        //        string smtpUser = Environment.GetEnvironmentVariable("SMTP_USER");
        //        string smtpPass = Environment.GetEnvironmentVariable("SMTP_PASS");
        //        string smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST") ?? "smtp.gmail.com";
        //        int smtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587");
        //        bool smtpSsl = bool.Parse(Environment.GetEnvironmentVariable("SMTP_SSL") ?? "true");

        //        NetworkCredential credential = new NetworkCredential
        //        {
        //            UserName = smtpUser,
        //            Password = smtpPass
        //        };

        //        MailMessage mail = new MailMessage
        //        {
        //            IsBodyHtml = true,
        //            From = new MailAddress(smtpUser)
        //        };
        //        mail.To.Add(user.UserEmail);
        //        mail.Subject = "Password Notification";
        //        mail.Body = builder.ToString();

        //        using (SmtpClient smtp = new SmtpClient
        //        {
        //            Host = smtpHost,
        //            UseDefaultCredentials = false,
        //            Credentials = credential,
        //            Port = smtpPort,
        //            EnableSsl = smtpSsl,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            Timeout = 20000
        //        })
        //        {
        //            smtp.Send(mail);
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //        LogErrorToRailway(ex);  // ✅ log to Railway
        //        return false;
        //    }
        //}

        //public static bool IsPasswordEmailSent(MailParameter user, out string message)
        //{
        //    message = string.Empty;

        //    try
        //    {
        //        var builder = new StringBuilder();

        //        // Path to the template file
        //        string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "_PasswordTemplate.cshtml");

        //        if (File.Exists(templatePath))
        //        {
        //            using (StreamReader reader = new StreamReader(templatePath))
        //            {
        //                builder.Append(reader.ReadToEnd());
        //            }

        //            builder.Replace("[realname]", user.RealName);
        //            builder.Replace("[username]", user.UserName);
        //            builder.Replace("[password]", user.UserPassword);
        //            builder.Replace("[company]", user.UserCompany);
        //            builder.Replace("[link]", string.Empty);
        //            builder.Replace("[year]", DateTime.Now.Year.ToString());
        //            builder.Replace("[reserved]", GlobalConstant.RESERVED);
        //        }
        //        else
        //        {
        //            // ✅ fallback email body if template not found
        //            builder.AppendLine($"Hello {user.RealName},");
        //            builder.AppendLine();
        //            builder.AppendLine("Your account has been created successfully.");
        //            builder.AppendLine($"Username: {user.UserName}");
        //            builder.AppendLine($"Password: {user.UserPassword}");
        //            builder.AppendLine();
        //            builder.AppendLine($"Company: {user.UserCompany}");
        //            builder.AppendLine($"{DateTime.Now.Year} {GlobalConstant.RESERVED}");
        //        }

        //        // ✅ Load credentials from Railway environment variables
        //        string smtpUser = Environment.GetEnvironmentVariable("SMTP_USER");
        //        string smtpPass = Environment.GetEnvironmentVariable("SMTP_PASS");
        //        string smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST") ?? "smtp.gmail.com";
        //        int smtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587");
        //        bool smtpSsl = bool.Parse(Environment.GetEnvironmentVariable("SMTP_SSL") ?? "true");

        //        NetworkCredential credential = new NetworkCredential
        //        {
        //            UserName = smtpUser,
        //            Password = smtpPass
        //        };

        //        MailMessage mail = new MailMessage
        //        {
        //            IsBodyHtml = true,
        //            From = new MailAddress(smtpUser)
        //        };
        //        mail.To.Add(user.UserEmail);
        //        mail.Subject = "Password Notification";
        //        mail.Body = builder.ToString();

        //        using (SmtpClient smtp = new SmtpClient
        //        {
        //            Host = smtpHost,
        //            UseDefaultCredentials = false,
        //            Credentials = credential,
        //            Port = smtpPort,
        //            EnableSsl = smtpSsl,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            Timeout = 20000
        //        })
        //        {
        //            smtp.Send(mail);
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //        LogErrorToRailway(ex);
        //        return false;
        //    }
        //}



        //    public static bool IsPasswordEmailSent(MailParameter user, out string message)
        //    {
        //        message = string.Empty;

        //        try
        //        {
        //            var builder = new StringBuilder();

        //            // Path to template file
        //            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "_PasswordTemplate.cshtml");

        //            if (File.Exists(templatePath))
        //            {
        //                using (StreamReader reader = new StreamReader(templatePath))
        //                {
        //                    builder.Append(reader.ReadToEnd());
        //                }

        //                builder.Replace("[realname]", user.RealName);
        //                builder.Replace("[username]", user.UserName);
        //                builder.Replace("[password]", user.UserPassword);
        //                builder.Replace("[company]", user.UserCompany);
        //                builder.Replace("[link]", string.Empty);
        //                builder.Replace("[year]", DateTime.Now.Year.ToString());
        //                builder.Replace("[reserved]", GlobalConstant.RESERVED);
        //            }
        //            else
        //            {
        //                // fallback body if template not found
        //                builder.AppendLine($"Hello {user.RealName},");
        //                builder.AppendLine();
        //                builder.AppendLine("Your account has been created successfully.");
        //                builder.AppendLine($"Username: {user.UserName}");
        //                builder.AppendLine($"Password: {user.UserPassword}");
        //                builder.AppendLine();
        //                builder.AppendLine($"Company: {user.UserCompany}");
        //                builder.AppendLine($"{DateTime.Now.Year} {GlobalConstant.RESERVED}");
        //            }

        //            // ✅ Load credentials from Railway environment variables
        //            string smtpUser = Environment.GetEnvironmentVariable("SMTP_USER"); // Brevo login (e.g. 98dd9b001@smtp-brevo.com)
        //            string smtpPass = Environment.GetEnvironmentVariable("SMTP_PASS"); // Brevo API key
        //            string smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST") ?? "smtp-relay.brevo.com";
        //            int smtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587");
        //            bool smtpSsl = bool.Parse(Environment.GetEnvironmentVariable("SMTP_SSL") ?? "true");

        //            NetworkCredential credential = new NetworkCredential
        //            {
        //                UserName = smtpUser,
        //                Password = smtpPass
        //            };

        //            // ✅ Must be a VERIFIED SENDER in Brevo
        //            string fromEmail = Environment.GetEnvironmentVariable("SMTP_FROM") ?? "no-reply@yourdomain.com";

        //            MailMessage mail = new MailMessage
        //            {
        //                IsBodyHtml = true,
        //From = new MailAddress(Environment.GetEnvironmentVariable("SMTP_FROM") ?? smtpUser, "Fintrak Software")
        //            };
        //            mail.To.Add(user.UserEmail);
        //            mail.Subject = "Password Notification";
        //            mail.Body = builder.ToString();

        //            using (SmtpClient smtp = new SmtpClient
        //            {
        //                Host = smtpHost,
        //                UseDefaultCredentials = false,
        //                Credentials = credential,
        //                Port = smtpPort,
        //                EnableSsl = smtpSsl,
        //                DeliveryMethod = SmtpDeliveryMethod.Network,
        //                Timeout = 20000
        //            })
        //            {
        //                smtp.Send(mail);
        //            }

        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            message = ex.Message;
        //            LogErrorToRailway(ex);
        //            return false;
        //        }
        //    }



        private static void LogErrorToRailway(Exception ex)
        {
            try
            {
                Console.Error.WriteLine("===== EMAIL ERROR =====");
                Console.Error.WriteLine($"Time: {DateTime.Now}");
                Console.Error.WriteLine($"Message: {ex.Message}");
                Console.Error.WriteLine($"StackTrace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.Error.WriteLine($"InnerException: {ex.InnerException.Message}");
                    Console.Error.WriteLine($"Inner Stack: {ex.InnerException.StackTrace}");
                }

                Console.Error.WriteLine("========================");
            }
            catch { }
        }
        public class EmailResult
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
        }

        public static async Task<EmailResult> IsPasswordEmailSent(MailParameter user)
        {
            var result = new EmailResult();

            try
            {
                var builder = new StringBuilder();

                // ✅ Load template if exists
                string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "_PasswordTemplate.cshtml");

                if (File.Exists(templatePath))
                {
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
                }
                else
                {
                    builder.AppendLine($"Hello {user.RealName},");
                    builder.AppendLine();
                    builder.AppendLine("Your account has been created successfully.");
                    builder.AppendLine($"Username: {user.UserName}");
                    builder.AppendLine($"Password: {user.UserPassword}");
                    builder.AppendLine();
                    builder.AppendLine($"Company: {user.UserCompany}");
                    builder.AppendLine($"{DateTime.Now.Year} {GlobalConstant.RESERVED}");
                }

                var apiKey = Environment.GetEnvironmentVariable("RESEND_API_KEY");
                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    result.Success = false;
                    result.Message = "Resend API key not found.";
                    return result;
                }

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                var payload = new
                {
                    from = "Ministry of Agric Oyo <onboarding@resend.dev>", // later replace with verified sender
                    to = new[] { user.UserEmail },
                    subject = "Password Notification",
                    html = builder.ToString()
                };

                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.resend.com/emails", content);
                var responseBody = await response.Content.ReadAsStringAsync();

                // ✅ Log to Railway
                Console.Error.WriteLine("===== RESEND EMAIL DEBUG =====");
                Console.Error.WriteLine($"Time: {DateTime.Now}");
                Console.Error.WriteLine($"To: {user.UserEmail}");
                Console.Error.WriteLine($"Payload: {json}");
                Console.Error.WriteLine($"Status: {response.StatusCode}");
                Console.Error.WriteLine($"Response: {responseBody}");
                Console.Error.WriteLine("================================");

                if (response.IsSuccessStatusCode)
                {
                    result.Success = true;
                    result.Message = "Email sent successfully.";
                    return result;
                }

                result.Success = false;
                result.Message = $"Resend API error: {responseBody}";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                LogErrorToRailway(ex);
                return result;
            }
        }

    }
}



