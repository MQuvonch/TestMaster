﻿using TestExecution.Service.DTOs.Option;

namespace TestExecution.Service.DTOs.Question;

public class QuestionFromResultDto
{
    public Guid Id { get; set; } 
    public string Text { get; set; }
    public List<OptionFromResultDto> Options { get; set; }
}
