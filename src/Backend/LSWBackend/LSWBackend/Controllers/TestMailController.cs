using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestMailController : Controller
    {
        private SendEmailsService _s;
        public TestMailController(SendEmailsService s)
        {
            _s = s;
        }
        [HttpGet("{email}")]
        public string SendTestmail(string email)
        {
            _s.SendTestmail(email);
            return "yay";
        }

        [HttpGet("angemeldet/{email}")]
        public string SendOfferSelectedmail(string email)
        {
            _s.SendOfferChoosen(email, "imaginary offer", DateTime.Now.ToShortDateString());

            return "yay";
        }
    }
}
