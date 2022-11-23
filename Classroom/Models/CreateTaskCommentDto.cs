using System.ComponentModel.DataAnnotations;

namespace Classroom.Models;

public class CreateTaskCommentDto
{
    [Required]
    public string? Comment { get; set; }
    public Guid? ParentId { get; set; }
}
