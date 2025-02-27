namespace TestExecution.Service.DTOs.UserAnswer;

public class UserAnswerFromResultDto
{
    public Guid QuestionId { get; set; }
    public Guid OptionId { get; set; }
    public Guid UserAttemptId { get; set; }
    public DateTime AnsweredAt { get; set; }
}
