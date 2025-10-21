//using System.ComponentModel.DataAnnotations;

//namespace TooliRent.DTO_s.ReservationDTOs
//{
//    public class CreateReservationDTO   // admin only??
//    {
//        [Required(ErrorMessage = "Start date is required")]
//        public DateTime Date2Hire { get; set; }

//        [Required(ErrorMessage = "End date is required")]
//        public DateTime Date2Return { get; set; }

//        [Required(ErrorMessage = "At least one tool must be selected")]
//        [MinLength(1, ErrorMessage = "At least one tool must be selected")]

//        [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
//        public string? Notes { get; set; }

//        public List<int> ToolIds { get; set; } = new List<int>();
//    }
//}
