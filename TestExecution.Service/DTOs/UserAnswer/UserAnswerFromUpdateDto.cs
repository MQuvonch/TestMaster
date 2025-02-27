namespace TestExecution.Service.DTOs.UserAnswer;

public class UserAnswerFromUpdateDto
{
    public Guid QuestionId { get; set; }
    public Guid OptionId { get; set; }
    public Guid UserAttemptId { get; set; }
}
