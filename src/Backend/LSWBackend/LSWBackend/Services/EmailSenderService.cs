using System.Net;
using System.Net.Mail;

using Microsoft.Extensions.Options;

namespace LSWBackend.Services;

public class EmailSenderService
{
    public SmtpClient SenderClient { get; set; } = new SmtpClient();

    private readonly string _smtpServer = "smtp.office365.com";
    private readonly string[] _credentials = Array.Empty<string>();
    private readonly AppSettings _appSettings;

    public EmailSenderService(IOptions<AppSettings> appSettings) {
        _appSettings = appSettings.Value;

        try {
            _credentials = File.ReadAllLines("Services/Email.txt");
        }
        catch (Exception) {
            Console.WriteLine("ERROR: Configuration-File for Email-Credentials not found");
        }

        try {
            SenderClient = new SmtpClient(_smtpServer) {
                Port = 587,
                Credentials = new NetworkCredential(_credentials[0], _credentials[1]),
                EnableSsl = true,
            };
        }
        catch (Exception e) {
            Console.WriteLine($"ERROR: Failed to init SmtpClient: {e.Message}");
        }
    }

    public bool SendMail(string recieverAddress, string subject, string message) {
        try {
            var mailMessage = new MailMessage {
                From = new MailAddress(_credentials[0], _appSettings.EmailAlias),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(recieverAddress);

            SenderClient.Send(mailMessage);

            return true;
        }
        catch (Exception e) {
            Console.WriteLine($"ERROR: Failed to send Email Exception: {e.Message}");
            return false;
        }
    }
}