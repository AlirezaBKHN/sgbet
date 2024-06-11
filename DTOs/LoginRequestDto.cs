using System.ComponentModel.DataAnnotations;

namespace sgbet.Dtos;

public class LoginRequest
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}