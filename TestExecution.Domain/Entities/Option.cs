using TestExecution.Domain.Commons;

namespace TestExecution.Domain.Entities;

public class Option:Auditable
{
    public Guid QuestionId { get; set; }    
    public Question Question { get; set; }
    public string Text { get; set; }
    public bool IsCorrect {  get; set; }  
}
