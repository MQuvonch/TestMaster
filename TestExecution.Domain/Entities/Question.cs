using TestExecution.Domain.Commons;

namespace TestExecution.Domain.Entities;

public class Question : Auditable
{
    public Guid TestId { get; set; }
    public Test Test { get; set; }
    public string Text {  get; set; }

    public ICollection<Option> OPtions { get; set; }
}
