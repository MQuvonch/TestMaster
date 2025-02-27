namespace TestExecution.Service.DTOs.UserAnswer;

public class SubmitTestCreationDto
{
    public Guid TestId { get; set; }

    public List<SubmitTestAnswerDetails> Details { get; set; }
}

public class SubmitTestAnswerDetails
{
    public Guid OptionId { get; set; }
};