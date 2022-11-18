using System.ComponentModel.DataAnnotations;

namespace Classroom.Models;

public class CreateCourceDto
{
    [Required]
    public string? Name { get; set; }
}