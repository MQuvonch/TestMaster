using System.ComponentModel.DataAnnotations.Schema;
using TestExecution.Domain.Commons;

namespace TestExecution.Domain.Entities;

public class Test : Auditable
{
    public Guid OwnerId { get; set; }
    [ForeignKey(nameof(OwnerId))]
    public User Owner { get; set; }
    public string Title {  get; set; }  
    public string Description { get; set; }
    public DateTime? Duration { get; set; }

    public List<Question> Questions { get; set; }
}
