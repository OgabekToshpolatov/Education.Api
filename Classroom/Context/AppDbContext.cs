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
    public DbSet<UserTask>? UserTasks { get; set; }
    public DbSet<TaskComment>? TaskComments { get; set; }
    public DbSet<LocalizedStringEntity>? LocalizedStrings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<LocalizedStringEntity>().HasKey(l => l.Key);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.Entity<LocalizedStringEntity>().HasData(
            new List<LocalizedStringEntity>()
            {
                new LocalizedStringEntity()
                {
                    Key = "Required",
                    Uz = "{0} kiritilishi kerak",
                    Ru = "{0} ruscha",
                    En = "{0} field is required"
                }
            });
    
    }

}