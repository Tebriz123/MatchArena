using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Exceptions
{
    public class ApplicationException : System.Exception
    {
        public ApplicationException(string message) : base(message) { }
    }
}
