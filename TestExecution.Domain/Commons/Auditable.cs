using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestExecution.Domain.Commons
{
    public class Auditable
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } 
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
