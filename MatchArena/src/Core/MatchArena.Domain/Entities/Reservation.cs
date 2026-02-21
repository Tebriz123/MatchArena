using MatchArena.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class Reservation:BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public long FieldId { get; set; }
        public Field Field { get; set; }
        public TimeOnly ReservedTime { get; set; }
        public DateTime ReservedDate { get; set; }
        public long? PaymentId { get; set; }
        public Payment Payment { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    }
}
