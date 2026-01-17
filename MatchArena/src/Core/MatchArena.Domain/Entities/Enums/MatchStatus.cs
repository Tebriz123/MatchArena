using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    internal enum MatchStatus
    {
        Pending = 1,
        Accepted = 2,
        Completed = 3,
        Cancelled = 4
    }
}
