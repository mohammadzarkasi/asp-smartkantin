using System.ComponentModel.DataAnnotations;

namespace smartkantin.Dto;

public class RegisterDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Minimal 3 karakter")]
    public string Username { get; set; } = "";
    
    [Required]
    [DataType(DataType.EmailAddress, ErrorMessage = "Email tidak valid")]
    public string Email { get; set; } = "";
    
    [Required]
    [MinLength(6, ErrorMessage = "Minimal 6 karakter")]
    public string Password { get; set; } = "";
}