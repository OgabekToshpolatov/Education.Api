using Classroom.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Classroom.Context;

public class AppDbContext:IdentityDbContext<User, Role, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
    public DbSet<Cource>? Cources { get; set; }
    public DbSet<UserCource>? UserCources { get; set; }
    public DbSet<Entities.Task>? Tasks { get; set; }

}