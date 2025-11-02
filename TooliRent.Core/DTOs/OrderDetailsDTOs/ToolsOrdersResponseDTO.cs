using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooliRent.Core.DTOs.OrderDetailsDTOs
{
    public class ToolsOrdersResponseDTO
    {
        public string Message { get; set; }
        public DateTime Date2Hire { get; set; }
        public DateTime Date2Return { get; set; }
        public string Status { get; set; }
        public List<OrderToolDTO> Tools { get; set; } = new List<OrderToolDTO>();
    }

    public class OrderToolDTO
    {
        public int OrderId { get; set; }
        public int ToolId { get; set; }
        public string ToolName { get; set; }
    }
}
