using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.Models;

namespace TooliRent.Core.Interfaces.IRepository
{
    public interface IReservationToolRepository
    {
        Task<ReservationTool> GetByIdAsync(int id);
        Task<List<ReservationTool>> GetAllAsync();
        Task<ReservationTool> AddAsync(ReservationTool reservationTool);
        Task UpdateAsync(ReservationTool reservationTool);
        Task DeleteAsync(int id);
    }
}
