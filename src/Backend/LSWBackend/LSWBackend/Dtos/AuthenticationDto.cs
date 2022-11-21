namespace LSWBackend.Dtos;

public class AuthenticationDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Role { get; set; } = null!;
    public int IsAdmin { get; set; }
    public string Token { get; set; } = null!;
}
