using System.ComponentModel.DataAnnotations.Schema;
using TestExecution.Service.DTOs.UserAnswer;

namespace TestExecution.Service.DTOs.UserAttempt;

public class UserAttemptFromCreateDto
{
    public Guid TestId { get; set; }
    public DateTime StartedAt { get; set; }
    public List<UserAnswerRangeCreateDto> Responses { get; set; }
}

