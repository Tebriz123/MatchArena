using MatchArena.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class Payment:BaseEntity
    {
        public string UserId { get; set; } = default!;
        public PaymentType Type { get; set; }
        public long? RelatedEntityId { get; set; }
        public long? RelatedProductId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "azn";
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public string? StripeSessionId { get; set; }
    }
}
