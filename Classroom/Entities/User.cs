using Microsoft.AspNetCore.Identity;

namespace Classroom.Entities;

public class User:IdentityUser<Guid>
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public virtual List<UserCource>? Cources { get; set; }
    public virtual List<UserTask>? UserTasks { get; set; }
}