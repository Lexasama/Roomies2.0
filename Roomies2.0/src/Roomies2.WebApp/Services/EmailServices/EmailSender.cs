using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Roomies2.WebApp.Services
{
    public   class EmailSender
    { 
        static IConfiguration _configuration;
       public string Path { get; }

        public static string SMTPAddress
        {
            get { return Configuration["EmailSender:SMTPAddress"]; }
        }

        public static int Port => int.Parse(Configuration["EmailSender:Port"]);

        public static bool SSL => bool.Parse(Configuration["EmailSender:SSL"]);

        public static string SenderEmail => Configuration["EmailSender:Email"];
        public static string Password => Configuration["EmailSender:Password"];
       
        static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
                }

                return _configuration;
            }
        }

        public EmailSender(string path)
        {
            Path = path;
        }
        public EmailSender()
        {

        }
        public  void SendEmail(string email)
        {
            string smtpAddress = Configuration["EmailSender:SMTPAddress"];
         
            string body = "bienvenue";
            //EmailContent(Path);

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(SenderEmail);
                mail.To.Add(email);
                mail.Subject = "Invitation à une colocation";
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(SMTPAddress, Port))
                {
                    smtp.Credentials = new NetworkCredential(SenderEmail, Password);
                    smtp.EnableSsl = SSL;

                    try
                    {
                        smtp.Send(mail);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception caugth in EmailSender.SendEmail to {0}:{1}", email, e);
                    }
                };

            };
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
