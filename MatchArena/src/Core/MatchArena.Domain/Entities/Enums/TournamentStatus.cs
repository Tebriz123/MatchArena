using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities.Enums
{
    public enum TournamentStatus
    {
        Draft = 1,
        RegistrationOpen = 2,
        RegistrationClosed = 3,
        InProgress = 4,
        Completed = 5,
        Cancelled = 6
    }
}
