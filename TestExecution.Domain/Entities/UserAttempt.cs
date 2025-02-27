using System.ComponentModel.DataAnnotations.Schema;
using TestExecution.Domain.Commons;

namespace TestExecution.Domain.Entities;

public class UserAttempt : Auditable
{
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    public Guid TestId { get; set; }
    [ForeignKey(nameof(TestId))]
    public Test Test { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime CompletedAt { get; set; }

    public ICollection<UserAnswer> UserAnswers { get; set; }

}
