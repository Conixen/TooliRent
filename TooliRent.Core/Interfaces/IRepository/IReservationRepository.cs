using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.Models;

namespace TooliRent.Core.Interfaces.IRepository
{
    public interface IReservationRepository
    {
        Task<Reservation> GetByIdAsync(int id);
        Task<List<Reservation>> GetAllAsync();
        Task<Reservation> AddAsync(Reservation reservation);


        Task AddReservationToolAsync(ReservationTool reservationTool);
        Task RemoveReservationToolAsync(int reservationId, int toolId);
    }
}
