using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TournamentStatus
    {
        Upcoming = 1,            
        RegistrationOpen = 2,    
        RegistrationClosed = 3,  
        SlotsFull = 4,           
        Ongoing = 5,            
        Finished = 6,             
        Cancelled = 7
    }
}
