using TestExecution.Domain.Entities;

namespace TestExecution.Service.DTOs.Question;

public class QuestionFromCreateDto
{
    public Guid TestId { get; set; }    
    public string Text { get; set; }
}
