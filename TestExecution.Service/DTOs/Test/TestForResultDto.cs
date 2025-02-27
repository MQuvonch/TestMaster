using System.ComponentModel.DataAnnotations.Schema;
using TestExecution.Domain.Entities;

namespace TestExecution.Service.DTOs.Test;

public class TestForResultDto
{
    public Guid Id { get; set; }    
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? Duration { get; set; }

}
