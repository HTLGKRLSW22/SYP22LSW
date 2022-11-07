﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LSWBackend.Services
{
    public class SendEmailsService
    {
        private EmailSenderService _email;
        public SendEmailsService(EmailSenderService email)
        {
            _email = email;

        }

        public void SendTestmail(string email)
        {
            string body = File.ReadAllText("Services/lws_email_default.html").Replace("$$TITLE", "Letzte Schulwoche Test Email")
                .Replace("$$CONTENT", "Dies ist eine Test Email der letzten Schulwocche");
            _email.SendMail(email, "Letzte Schulwoche Testmail", body);
        }

        public void SendOfferChoosen(string email, string offer, string day)
        {
            string body = File.ReadAllText("Services/lws_email_default.html").Replace("$$TITLE", $"Letzte Schulwoche {offer} angemeldet")
                .Replace("$$CONTENT", $"Du hast dich für den Kurs {offer} am {day} Angemeldet. <br> <br> " +
                                      $"Du wirst am Ende der Anmeldefrist nochmal informiert ob dein Kurs zustande kommt. " +
                                      $"Wenn du beim Anmelden schon gesehen hast dass die Mindestteilnehmeranzahl erreicht ist," +
                                      $" dann besteht eine gute Chance, dass der Kurs zustande kommt." +
                                      $" Wenn der Kurs nicht zustande kommt wirst du auch am Ende der Anmeldefrist informiert" +
                                      $" und du muss dich dann für einen anderen Kurs anmelden.");
            _email.SendMail(email, $"Letzte Schulwoche {offer} angemeldet", body);
        }

        public void SendSignUpStart(string email, string endingdate)
        {
            string body = File.ReadAllText("Services/lws_email_default.html")
                .Replace("$$TITLE", $"Letzte Schulwoche Anmeldebeginn")
                .Replace("$$CONTENT", $"<h1> https://www.sharepointlist.com </h1>" +
                                      $"<br> <br><span class=\"warning\"> Anmeldeschluss ist der {endingdate}!</span> <br> <br>" +
                                      $"Du kannst dich jetzt für Kurse eintragen. Bitte melde dich vor dem {endingdate} an," +
                                      $" ansonsten wirst du einem Kurs zugeteilt.");
            _email.SendMail(email, $"Letzte Schulwoche anmelden bis {endingdate}", body);
        }

        public void SendAbcence(string email, string absencedate)
        {
            string body = File.ReadAllText("Services/lws_email_default.html")
                .Replace("$$TITLE", $"Du wurdest für {absencedate} freigestellt")
                .Replace("$$CONTENT", $"Du musst dich nicht mehr für den {absencedate} freigestellt. " +
                                      $"Du musst dich nicht mehr für diesen Tag anmelden " +
                                      $"und wurdest abgemeldet fallst du dich schon für einen Kurs angemeldet hast");
            _email.SendMail(email, $"Letzte Schulwoche Freistellung {absencedate}", body);
        }
    }
}