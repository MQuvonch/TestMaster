using System.ComponentModel.DataAnnotations.Schema;

namespace TestExecution.Service.DTOs.UserAttempt;

public class UserAttemptFromCreateDto
{
    public Guid TestId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime CompletedAt { get; set; }

}
