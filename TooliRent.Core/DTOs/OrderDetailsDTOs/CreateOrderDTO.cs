using System.ComponentModel.DataAnnotations;

namespace TooliRent.DTO_s.OrderDetailsDTOs
{
    public class CreateOrderDTO // ADMIN ONLY
    {
        public DateTime Date2Hire { get; set; }
        public DateTime Date2Return { get; set; }
        public int ToolId { get; set; }

    }
}
