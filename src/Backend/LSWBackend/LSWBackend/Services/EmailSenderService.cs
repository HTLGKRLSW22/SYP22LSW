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
    private readonly Queue<MailMessage> _mailList = new();

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
            SendMailAsync();
        }
        catch (Exception e) {
            Console.WriteLine($"ERROR: Failed to init SmtpClient: {e.Message}");
        }
    }

    public bool SendMail(string recieverAddress, string subject, string message) {

        try {
            var mail = new MailMessage {
                From = new MailAddress(_credentials[0], _appSettings.EmailAlias),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            mail.To.Add(recieverAddress);
            _mailList.Enqueue(mail);
            return true;
        }
        catch (Exception e) {
            Console.WriteLine($"ERROR: Failed to send Mail: {e.Message}");
            return false;
        }

    }

    public async Task SendMailAsync() {
        while (true) {
            if (_mailList.Count > 0) {
                MailMessage mail = _mailList.Dequeue();
                try {
                    await SenderClient.SendMailAsync(mail);
                }
                catch (Exception e) {
                    Console.WriteLine($"ERROR: Failed to send Mail: {e.Message}");
                }
            }
            await Task.Delay(300000);
        }
    }
}
