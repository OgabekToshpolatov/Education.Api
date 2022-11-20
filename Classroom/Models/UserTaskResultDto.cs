using Classroom.Entities;

namespace Classroom.Models;

public class UserTaskResultDto : TaskDto
{
    public UserTaskResult? UserResult { get; set; }
}

public class UserTaskResult
{
    public string? Description { get; set; }

    public EUserTaskStatus Status { get; set; }
}