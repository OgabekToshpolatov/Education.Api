using Classroom.Entities;

namespace Classroom.Models;

public class CreateTaskDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int MaxScore { get; set; }
    public ETaskStatus Status { get; set; }
}