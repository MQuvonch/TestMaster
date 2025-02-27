using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExecution.Domain.Entities;

namespace TestExecution.Service.DTOs.Test
{
    public class TestForCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Duration { get; set; }
    }
}
