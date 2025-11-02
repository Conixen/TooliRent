using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooliRent.Core.DTOs.StatisticksDTOs
{
    public class StatisticsDTO
    {
        public int TotalUsers { get; set; }
        public int TotalTools { get; set; }
        public int TotalOrders { get; set; }
        public int ActiveOrders { get; set; }
        public int CompletedOrders { get; set; }
    }
}
