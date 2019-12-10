using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Roomies2.WebApp.Services
{
    public class EmailSender
    {
        public IConfiguration Configuration {get;}
        public string Path { get; }

        public EmailSender(IConfiguration configuration, string path)
        {
            Configuration = configuration;
            Path = path;
        }

        public void SendEmail( string email)
        {
            string smtpAddress = Configuration["Emailsender:SMTPAddress"];
            int portNumber = int.Parse(Configuration["EmailSender:PortNumber"]);
            bool enableSsl = bool.Parse(Configuration["EmailSender:EnableSSL"]);
            string emailFromAddress = Configuration["EmailSender:Email"];
            string password = Configuration["EmailSender:Password"];
            string body = EmailContent(Path);

            using MailMessage mail = new MailMessage { From = new MailAddress(emailFromAddress) };
            mail.To.Add(email);
            mail.Subject = "Invitation à une colocation";
            mail.Body = body;
            mail.IsBodyHtml = true;

            using SmtpClient smtp = new SmtpClient(smtpAddress, portNumber)
            {
                Credentials = new NetworkCredential(emailFromAddress, password), EnableSsl = enableSsl
            };

            try
            {
                smtp.Send(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caugth in EmailSender.SendEmail to {0}:{1}",email, e);
            }
        }

        public static string EmailContent(string path)
        {
            using (StreamReader reader = File.OpenText(path))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
