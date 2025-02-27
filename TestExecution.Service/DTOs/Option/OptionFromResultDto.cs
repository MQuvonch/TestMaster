namespace TestExecution.Service.DTOs.Option;

public class OptionFromResultDto
{
    public Guid QuestionId { get; set; }
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
}
