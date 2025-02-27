using System.ComponentModel.DataAnnotations.Schema;

namespace TestExecution.Service.DTOs.UserAnswer;

public class UserAnswerFromCreateDto
{
    public Guid QuestionId { get; set; }
    public Guid OptionId { get; set; }
    public Guid UserAttemptId { get; set; }
    public DateTime AnsweredAt { get; set; }
}
