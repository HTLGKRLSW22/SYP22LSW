namespace LSWBackend.Dtos;

public class AuthenticationDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
    public int IsAdmin { get; set; }
    public string Token { get; set; }
}
