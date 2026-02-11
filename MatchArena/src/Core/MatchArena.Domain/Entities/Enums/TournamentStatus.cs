using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public enum TournamentStatus
    {
        RegistrationOpen= 1,  
        RegistrationClosed = 2, 
        SlotsFull = 3,          
        Cancelled = 4
    }
}
