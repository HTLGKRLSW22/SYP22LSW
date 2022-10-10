using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        static HttpClient client = new HttpClient();
        OfferService _service;

        public AuthenticationController()
        {
            this._service = new OfferService();
        }

        [HttpGet("login")]
        public ActionResult<string[]> Login(string username, string password)
        {
            //username und passwort wird von der B Klasse abgeprüft hierfür brauche ich dann nur einen get request der true oder false zurückgibt
            //HttpResponseMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:44300/api/authentication/login?{username}{password}");
            return Ok("test;test");
        }
    }
}
