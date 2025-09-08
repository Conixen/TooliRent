namespace TooliRent.Models
{
    public class ReservationTool
    {
        public int Id { get; set; }
        public int ToolId { get; set; }
        public int ReservationId { get; set; }

        // foreign key
        public Tool Tool { get; set; }
        public Reservation Reservation { get; set; }
    }
}
