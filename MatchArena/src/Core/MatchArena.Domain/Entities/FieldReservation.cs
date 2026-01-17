using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class FieldReservation:BaseEntity
    {
        public DateTime ReservedAt { get; set; }

        //reletion
        public long FieldScheduleId { get; set; }
        public FieldSchedule Schedule { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
