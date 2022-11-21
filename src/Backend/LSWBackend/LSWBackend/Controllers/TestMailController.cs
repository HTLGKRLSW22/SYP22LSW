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
        return _s.SendTestmail(email) ? "Email sent" : "Email could not be sent";
    }

    [HttpGet("Anmeldung/{email}")]
    public string SendOfferSelectedmail(string email) {
        return _s.SendOfferChoosen(email, "imaginary offer", DateTime.Now.ToShortDateString()) ? "Email sent" : "Email could not be sent";
    }

    [HttpGet("AnmeldezeitraumBegonnen/{email}")]
    public string SendwelcomeEmail(string email) {
        return _s.SendSignUpStart(email, "12.12.2022") ? "Email sent" : "Email could not be sent";
    }

    [HttpGet("Freistellung/{email}")]
    public string SendAbsenceMail(string email) {
        return _s.SendAbcence(email, "01.12.2022") ? "Email sent" : "Email could not be sent";
    }

    [HttpGet("ZeitraumEndet/{email}")]
    public string SendWarningEndingSoon(string email) {
        bool success;

        //Mehrere Tage
        string[] dates = new string[] { "01.12.2022", "02.12.2022" };
        _s.SendWarningTimeEndingSoon(email, "16.11.2022", dates);

        string[] date = new string[] { "01.12.2022" };
        success = _s.SendWarningTimeEndingSoon(email, "16.11.2022", date);

        return success ? "Emails sent" : "Emails could not be sent";
    }

    [HttpGet("KursNichtZustandeGekommen/{email}")]
    public string SendCourseFailed(string email) {
        return _s.SendNotificationCourseFailed(email, "Testkurs") ? "Email sent" : "Email could not be sent";
    }

    [HttpGet("LehrerKeinKursKeineZuteilung/{email}")]
    public string SendNoCourseNoAssignment(string email) {
        return _s.SendNotificationNoCourseOrNoAssignment(email) ? "Email sent" : "Email could not be sent";
    }

    [HttpGet("TestAlle/{email}")]
    public string SendAll(string email) {
        bool success;

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

        success = _s.SendNotificationNoCourseOrNoAssignment(email);

        return success ? "Emails sent" : "Emails could not be sent";
    }
}
