using System.ComponentModel.DataAnnotations;

namespace Classroom.Models;

public class UpdateCourceDto
{
    [Required]
    public string? Name { get; set; }
}
