using System.ComponentModel.DataAnnotations.Schema;
using TestExecution.Domain.Commons;

namespace TestExecution.Domain.Entities;

public class UserAnswer : Auditable
{
    public Guid QuestionId { get; set; }
    [ForeignKey(nameof(QuestionId))]
    public Question Question { get; set; }
    public Guid OptionId { get; set; }
    [ForeignKey(nameof(OptionId))]
    public Option Option { get; set; }
    public Guid UserAttemptId { get; set; }
    [ForeignKey(nameof(UserAttemptId))]
    public UserAttempt UserAttempt { get; set; }
    public DateTime AnsweredAt { get; set; }
}
