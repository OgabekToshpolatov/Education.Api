using Classroom.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Classroom.Context.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<Entities.User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);
        builder.Property(user => user.Firstname).IsRequired();

        builder.HasMany(user => user.Cources)
            .WithOne(userCourse => userCourse.User)
            .HasForeignKey(userCourse=>userCourse.UserId);
    }
}