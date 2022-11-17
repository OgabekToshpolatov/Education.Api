using System.ComponentModel.DataAnnotations;

namespace Classroom.Models;

public class SignUpUserDto
{
   [Required]
   public string? UserName { get; set; }
   [Required]
   public string? Firstname { get; set; }
   public string? Lastname { get; set; }
   [Required]
   public string? Password { get; set; }
   public string? ConfirmPassword { get; set; }
}