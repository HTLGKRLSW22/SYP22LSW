using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class TestMailController : Controller
{
    private readonly SendEmailsService _s;
    public TestMailController(SendEmailsService s) => _s = s;

    [HttpGet("{email}")]
    public string SendTestmail(string email) {
        _s.SendTestmail(email);
        return "Email send";
    }

    [HttpGet("Anmeldung/{email}")]
    public string SendOfferSelectedmail(string email) {
        _s.SendOfferChoosen(email, "imaginary offer", DateTime.Now.ToShortDateString());
        return "Email send";
    }

    [HttpGet("AnmeldezeitraumBegonnen/{email}")]
    public string SendwelcomeEmail(string email) {
        _s.SendSignUpStart(email, "12.12.2022");
        return "Email send";
    }

    [HttpGet("Freistellung/{email}")]
    public string SendAbsenceMail(string email) {
        _s.SendAbcence(email, "01.12.2022");
        return "Email send";
    }

    [HttpGet("ZeitraumEndet/{email}")]
    public string SendWarningEndingSoon(string email) {
        //Mehrere Tage
        string[] dates = new string[] { "01.12.2022", "02.12.2022" };
        _s.SendWarningTimeEndingSoon(email, "16.11.2022", dates);

        string[] date = new string[] { "01.12.2022" };
        _s.SendWarningTimeEndingSoon(email, "16.11.2022", date);

        return "Email send";
    }

    [HttpGet("KursNichtZustandeGekommen/{email}")]
    public string SendCourseFailed(string email) {
        _s.SendNotificationCourseFailed(email, "Testkurs");
        return "Email send";
    }

    [HttpGet("LehrerKeinKursKeineZuteilung/{email}")]
    public string SendNoCourseNoAssignment(string email) {
        _s.SendNotificationNoCourseOrNoAssignment(email);
        return "Email send";
    }

    [HttpGet("TestAlle/{email}")]
    public string SendAll(string email) {
        _s.SendTestmail(email);

        _s.SendOfferChoosen(email, "imaginary offer", DateTime.Now.ToShortDateString());

        _s.SendSignUpStart(email, "12.12.2022");

        _s.SendAbcence(email, "01.12.2022");

        //Mehrere Tage
        string[] dates = new string[] { "01.12.2022", "02.12.2022" };
        _s.SendWarningTimeEndingSoon(email, "16.11.2022", dates);

        string[] date = new string[] { "01.12.2022" };
        _s.SendWarningTimeEndingSoon(email, "16.11.2022", date);

        _s.SendNotificationCourseFailed(email, "Testkurs");

        _s.SendNotificationNoCourseOrNoAssignment(email);

        return "Emails send";
    }
}
