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

    [HttpPost("login")]
    public ActionResult<string[]> Login([FromBody] LoginDto login) {
        //login und passwort wird von der B Klasse abgeprüft hierfür brauche ich dann nur einen get request der true oder false zurückgibt
        //HttpResponseMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:44300/api/authentication/login?{login}{password}");
        //ID Username

        var teacher = new Teacher() { Username = login.Username, IsAdmin = 0, FirstName = "test", LastName = "test", TeacherId = 1 };
        //var student = new Student() { Username = login.Username, FirstName = "test", LastName = "test", StudentId = 1 };
        //var admin = new Teacher() { Username = login.Username, IsAdmin = 1, FirstName = "test", LastName = "test", TeacherId = 1 };

        string token = CreateTokenString(teacher);
        //string token2 = CreateTokenString(student);
        //string token3 = CreateTokenString(admin);

        //student
        return Ok(new AuthenticationDto() {
            Id = teacher.TeacherId,
            IsAdmin = teacher.IsAdmin,
            Role = "student",
            Token = token,
            Username = teacher.Username
        });

        //student
        //return Ok(new AuthenticationDto() {
        //    Id = student.StudentId,
        //    Role = "student",
        //    Token = token2,
        //    Username = student.Username
        //});

        //admin
        //return Ok(new AuthenticationDto() {
        //    Id = admin.TeacherId,
        //    IsAdmin = admin.IsAdmin,
        //    Role = "student",
        //    Token = token3,
        //    Username = admin.Username
        //});
    }

    private string CreateTokenString(Teacher teacher) {
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, teacher.TeacherId.ToString()),
                new Claim(ClaimTypes.Name, teacher.Username),
                new Claim( ClaimTypes.Role,"student"),
            }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }

    //private string CreateTokenString(Student student) {
    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
    //    var tokenDescriptor = new SecurityTokenDescriptor {
    //        Subject = new ClaimsIdentity(new[]
    //        {
    //            new Claim(ClaimTypes.NameIdentifier, student.StudentId.ToString()),
    //            new Claim(ClaimTypes.Name, student.Username),
    //            new Claim( ClaimTypes.Role,"student"),
    //        }),
    //        Expires = DateTime.UtcNow.AddHours(4),
    //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //    };
    //    var token = tokenHandler.CreateToken(tokenDescriptor);
    //    string tokenString = tokenHandler.WriteToken(token);
    //    return tokenString;
    //}
}
