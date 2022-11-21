using System.Net;
using System.Net.Mail;

namespace LSWBackend.Services;

public class EmailSenderService
{
    public SmtpClient SenderClient { get; set; } = new SmtpClient();

    private readonly string _smtpServer = "smtp.office365.com";
    private readonly string[] _credentials = Array.Empty<string>();

    public EmailSenderService() {
        try {
            _credentials = File.ReadAllLines("Services/Email.txt");
            SenderClient = new SmtpClient(_smtpServer) {
                Port = 587,
                Credentials = new NetworkCredential(_credentials[0], _credentials[1]),
                EnableSsl = true,
            };
        }
        catch (Exception e) {
            Console.WriteLine($"Failed to init SmtpClient: {e.Message}");
        }
    }

    public bool SendMail(string recieverAddress, string subject, string message) {
        try {
            var mailMessage = new MailMessage {
                From = new MailAddress(_credentials[0]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(recieverAddress);

            SenderClient.Send(mailMessage);

            return true;
        }
        catch (Exception e) {
            Console.WriteLine($"Failed to send Email Exception: {e.Message}");
        }

        return false;
    }
}
