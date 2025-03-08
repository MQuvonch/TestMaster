using TestExecution.Service.DTOs.UserAnswer;

namespace TestExecution.Service.DTOs.UserAttempt;

public class UserAttemptFromResultDto
{
    public Guid TestId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime CompletedAt { get; set; }
    public int CorrectAnswersCount { get; set; }
    public int IsCorrectAnswersCount {  get; set; } 
}
