namespace LSWBackend.Dtos;

public class AuthenticationDto
{
    [Required] public int Id { get; set; }
    [Required] public string Username { get; set; } = null!;
    [Required] public string Role { get; set; } = null!;
    [Required] public int IsAdmin { get; set; }
    [Required] public string Token { get; set; } = null!;
}

