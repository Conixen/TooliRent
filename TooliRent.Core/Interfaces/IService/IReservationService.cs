using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.Models;

namespace TooliRent.Core.Interfaces.IService
{
    public interface IReservationService
    {
        Task<Reservation> GetByIdAsync(int id);
        Task<List<Reservation>> GetAllAsync();
        Task<Reservation> AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(int id);
    }
}
