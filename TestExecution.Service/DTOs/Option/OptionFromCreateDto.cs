namespace TestExecution.Service.DTOs.Option;

public class OptionFromCreateDto
{
    public Guid QuestionId { get; set; }
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
}
