using System.ComponentModel.DataAnnotations;

namespace TooliRent.DTO_s.OrderDetailsDTOs
{
    public class CreateOrderDTO 
    {
        public DateTime Date2Hire { get; set; }
        public DateTime Date2Return { get; set; }
        public List<int> ToolIds { get; set; } = new List<int>();   // så you can order 1 or more tools

    }
}
