using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooliRent.Core.DTOs.CommonDTOs
{
    public class AvailabilityCheckDTO
    {
        [Required]
        public List<int> ToolIds { get; set; } = new List<int>();

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int? ExcludeReservationId { get; set; }
    }
}
