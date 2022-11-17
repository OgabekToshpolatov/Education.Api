using System.ComponentModel.DataAnnotations;

namespace Classroom.Models;

public class SignInUserDto
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }
}