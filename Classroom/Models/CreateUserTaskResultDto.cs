using Classroom.Entities;

namespace Classroom.Models;

public class CreateUserTaskResultDto
{
    public string? Description { get; set; }
    public EUserTaskStatus Status { get; set; }
}