using MatchArena.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class TournamentRegistration:BaseEntity
    {

        public long TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public long TeamId { get; set; }
        public Team Team { get; set; }
        public string CaptainUserId { get; set; }
        public AppUser Captain { get; set; }
        public long? PaymentId { get; set; }
        public Payment Payment { get; set; }
        public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;
    }
}
