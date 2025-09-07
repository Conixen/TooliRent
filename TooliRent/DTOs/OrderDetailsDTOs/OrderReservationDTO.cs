namespace TooliRent.DTO_s.OrderDetailsDTOs
{
    public class OrderReservationDTO
    {
        public OrderReservationDTO Reservation { get; set; } = null!;

        public bool IsOverdue { get; set; }
        public int DaysOverdue { get; set; }
        public int RentalDays { get; set; }
        public bool CanBeCancelled { get; set; }
        public bool CanBeCheckedOut { get; set; }
        public bool CanBeReturned { get; set; }
    }
}
