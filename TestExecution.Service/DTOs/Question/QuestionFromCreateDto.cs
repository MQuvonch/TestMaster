public class QuestionRangeCreateDto
{
    public Guid TestId { get; set; }
    public List<QuestionCreateDto> Questions { get; set; }
}

public class QuestionCreateDto()
{
    public string Text { get; set; }
    public List<OptionCreateDto> Options { get; set; }
   
}

public class OptionCreateDto()
{
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
}