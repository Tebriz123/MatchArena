using MatchArena.Domain.Entities.Enums;

namespace MatchArena.MVC.ViewModels.Reservation
{
    public class GetReservationVM
    {
        public long Id { get; set; }
        public long FieldId { get; set; }
        public string FieldName { get; set; } = null!;
        public TimeOnly ReservedTime { get; set; }
        public DateTime ReservedDate { get; set; }
        public ReservationStatus Status { get; set; }
        public decimal Amount { get; set; }
    }
}
