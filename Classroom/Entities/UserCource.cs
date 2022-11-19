using System.ComponentModel.DataAnnotations.Schema;

namespace Classroom.Entities;

public class UserCource
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }

    public Guid CourceId { get; set; }
    [ForeignKey(nameof(CourceId))]
    public virtual Cource? Cource { get; set; }

    public bool IsAdmin { get; set; }
}