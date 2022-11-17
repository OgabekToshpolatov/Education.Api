using Microsoft.AspNetCore.Identity;

namespace Classroom.Entities;

public class User:IdentityUser<Guid>
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
}