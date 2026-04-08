namespace MatchArena.MVC.ViewModels.Reservation
{
    public class PostReservationVM
    {
        public long FieldId { get; set; }
        public TimeOnly ReservedTime { get; set; }
        public DateTime ReservedDate { get; set; }
    }
}
