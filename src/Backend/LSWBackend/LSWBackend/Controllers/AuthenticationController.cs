using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using LSWBackend.Dtos;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LSWBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    //private readonly static HttpClient client = new();
    private readonly AppSettings _appSettings;

    public AuthenticationController(IOptions<AppSettings> appSettings) => _appSettings = appSettings.Value;

    [HttpGet("login")]
    public ActionResult<string[]> Login(string username) {
        //username und passwort wird von der B Klasse abgeprüft hierfür brauche ich dann nur einen get request der true oder false zurückgibt
        //HttpResponseMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:44300/api/authentication/login?{username}{password}");
        //ID Username

        var teacher = new Teacher() { Username = username, IsAdmin = 1, FirstName = "test", LastName = "test", TeacherId = 1 };

        string token = CreateTokenString(teacher);

        return Ok(new AuthenticationDto() {
            Id = teacher.TeacherId,
            IsAdmin = teacher.IsAdmin,
            Role = "teacher",
            Token = token,
            Username = teacher.Username
        });
    }

    private string CreateTokenString(Teacher teacher) {
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, teacher.TeacherId.ToString()),
                new Claim(ClaimTypes.Name, teacher.Username),
                new Claim( ClaimTypes.Role,"teacher"),
            }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }
}
