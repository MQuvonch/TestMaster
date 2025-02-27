using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExecution.Service.Exceptions
{
    public class TestCustomException : Exception
    {
        public int StatusCode { get; set; }
        public TestCustomException(int statusCode, string Message) : base(Message)
        {
            StatusCode = statusCode;
        }
    }
}
