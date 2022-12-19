using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LSWBackend.Services;

public class SendEmailsService
{
    private readonly EmailSenderService _email;

    private const string PahtDefaultEmail = "Services/lws_email_default.html";
    private const string PahtWarningEmail = "Services/lws_email_warning.html";

    public SendEmailsService(EmailSenderService email) => _email = email;

    public bool SendTestmail(string email) {
        string body = File.ReadAllText(PahtDefaultEmail).Replace("$$TITLE", "Letzte Schulwoche Test Email")
            .Replace("$$CONTENT", "Dies ist eine Test Email der letzten Schulwocche");

        return _email.SendMail(email, "Letzte Schulwoche: Testmail", body);
    }

    public bool SendOfferChoosen(string email, string offer, string day) {
        string body = File.ReadAllText(PahtDefaultEmail).Replace("$$TITLE", $"Letzte Schulwoche: Bei \"{offer}\" angemeldet")
            .Replace("$$CONTENT", $"Du hast dich für den Kurs {offer} am {day} angemeldet. <br> <br> " +
                                  $"Du wirst am Ende der Anmeldefrist nochmal informiert ob dein Kurs zustande kommt. " +
                                  $"Wenn der Kurs nicht zustande kommt wirst du am Ende der Anmeldefrist informiert" +
                                  $" und muss dich dann für einen anderen Kurs anmelden.");
        return _email.SendMail(email, $"Letzte Schulwoche: Anmeldung erfolgreich", body);
    }

    public bool SendSignUpStart(string email, string endingdate) {
        string body = File.ReadAllText(PahtDefaultEmail)
            .Replace("$$TITLE", $"Letzte Schulwoche: Anmeldebeginn")
            .Replace("$$CONTENT", $"<h1> https://www.sharepointlist.com </h1>" +
                                  $"Du kannst dich jetzt für Kurse eintragen. Bitte melde dich vor dem {endingdate} an," +
                                  $" ansonsten wirst du einem Kurs zugeteilt." +
                                   $"<br> <br><span class=\"warning\"> Anmeldeschluss ist der {endingdate}!</span> <br> <br>");
        return _email.SendMail(email, $"Letzte Schulwoche: Anmeldezeitraum bis {endingdate}", body);
    }

    public bool SendAbcence(string email, string absencedate) {
        string body = File.ReadAllText(PahtDefaultEmail)
            .Replace("$$TITLE", $"Du wurdest für den {absencedate} freigestellt")
            .Replace("$$CONTENT", $"Du musst dich nicht mehr für den {absencedate} freigestellt. " +
                                  $"Du musst dich nicht mehr für diesen Tag anmelden " +
                                  $"und wurdest abgemeldet fallst du dich schon für einen Kurs angemeldet hast");
        return _email.SendMail(email, $"Letzte Schulwoche: Freistellung eingetragen", body);
    }

    public bool SendWarningTimeEndingSoon(string email, string enddate, string[] datesNotEnrolled) {

        string dates = datesNotEnrolled.Length == 1 ? $"den {datesNotEnrolled[0]}" : $"die Tage {string.Join(", ", datesNotEnrolled)}";

        string body = File.ReadAllText(PahtWarningEmail)
            .Replace("$$TITLE", $"Du kannst dich noch bis zum {enddate} eintragen")
            .Replace("$$CONTENT", $"Du hast dich noch nicht für {dates} eingetragen. " +
                                  $"Du musst dich bist zum {enddate} eintragen haben" +
                                  $"<br> Wenn du dich nicht bis zum {enddate} einträgst wirdst du einem Kurs zugeteilt");

        return _email.SendMail(email, $"Letzte Schulwoche: Fehlende Anmeldung(en)", body);
    }

    public bool SendNotificationCourseFailed(string email, string course) {
        string body = File.ReadAllText(PahtDefaultEmail)
           .Replace("$$TITLE", $"Der Kurs ist nicht zustande gekommen")
           .Replace("$$CONTENT", $"Du hast dich für den Kurs {course} eingetragen. " +
                                 $"Jedoch ist dieser nicht zustande gekommen. " +
                                 $"Bitte wähle einen anderen Kurs aus.");

        return _email.SendMail(email, $"Letzte Schulwoche: Kurs nicht zustande gekommen", body);
    }

    public bool SendNotificationNoCourseOrNoAssignment(string email) {
        string body = File.ReadAllText(PahtDefaultEmail)
          .Replace("$$TITLE", $"Kein Kurs/keine Zuteilung")
          .Replace("$$CONTENT", $"Sie haben noch keinen Kurs erstellt, oder sind noch keinem zugeteilt. " +
                                $"Bitte erstellen Sie einen Kurs oder lassen Sie sich als Begleitperson zuteilen.");

        return _email.SendMail(email, $"Letzte Schulwoche: Noch kein Kurs/keine Zuteilung", body);
    }
}
