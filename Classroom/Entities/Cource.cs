namespace Classroom.Entities;

public class Cource
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Key { get; set; }

    public virtual List<UserCource>? Users { get; set; }
    public virtual List<Task>? Tasks { get; set; }
}