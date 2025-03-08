using System.ComponentModel.DataAnnotations.Schema;

namespace TestExecution.Service.DTOs.UserAnswer;

public class UserAnswerRangeCreateDto
{
    public Guid QuestionId { get; set; }
    public Guid OptionId { get; set; }
    public DateTime AnsweredAt { get; set; } 
}








