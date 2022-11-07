using System.Net;
using System.Net.Mail;

namespace LSWBackend.Services
{
    public class EmailSenderService
    {
        public SmtpClient SenderClient { get; set; } = new SmtpClient();

        private string smtpServer = "smtp-mail.outlook.com";
        private string[] credentials;

        public EmailSenderService()
        {
            try
            {
                credentials = File.ReadAllLines("Services/Email.txt");
                SenderClient = new SmtpClient(smtpServer)
                {
                    Port = 587,
                    Credentials = new NetworkCredential(credentials[0], credentials[1]),
                    EnableSsl = true,
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to init SmtpClient: {e.Message}");
            }
        }

        public bool SendMail(string recieverAddress, string subject, string message)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(credentials[0]),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(recieverAddress);

                SenderClient.Send(mailMessage);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to send Email Exception: {e.Message}");
            }

            return false;
        }
    }
}
