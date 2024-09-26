using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCase.Application.Exceptions
{
    public class InvalidFilterException : Exception
    {
        public InvalidFilterException(string message) : base(message)
        {
            
        }
    }
}
