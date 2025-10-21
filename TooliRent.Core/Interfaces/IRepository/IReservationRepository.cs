//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TooliRent.Models;

//namespace TooliRent.Core.Interfaces.IRepository
//{
//    public interface IReservationRepository
//    {
//        Task<Reservation> GetByIdAsync(int id);
//        Task<List<Reservation>> GetByUserIdAsync(int userId);

//        Task<List<Reservation>> GetAllAsync();
//        Task<Reservation> AddAsync(Reservation reservation);
//        Task UpdateAsync(Reservation reservation);
//        Task DeleteAsync(int id);

//        // ReservationTool
//        Task AddReservationToolAsync(ReservationTool reservationTool);
//        Task RemoveReservationToolAsync(int reservationId, int toolId);
//        Task<List<ReservationTool>> GetReservationToolsAsync(int reservationId);

//        // new ones
//        Task ClearReservationToolsAsync(int reservationId);
//        Task<bool> AreToolsAvailableAsync(List<int> toolIds, DateTime startDate, DateTime endDate, int? excludeReservationId = null);
//        Task<List<Reservation>> GetActiveReservationsAsync();

//    }
//}
