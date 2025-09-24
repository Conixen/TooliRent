using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.DTO_s.ReservationDTOs;
using TooliRent.Models;

namespace TooliRent.Core.Interfaces.IService
{
    public interface IReservationService
    {
        // Basic CRUD
        Task<ReservationDetailDTO?> GetByIdAsync(int id);
        Task<List<ReservationSummaryDTO>> GetAllAsync();
        Task<List<ReservationSummaryDTO>> GetByUserIdAsync(int userId);
        Task DeleteAsync(int id);

        // Create & Update
        Task<ReservationDetailDTO> CreateAsync(CreateReservationDTO dto, int userId);
        Task UpdateAsync(int id, UpdateReservationDTO dto);
        Task CancelAsync(int id, ReservationCancelDTO dto);

        // Business logic
        Task<bool> CanUserCancelAsync(int reservationId, int userId);
        Task<bool> CanUserModifyAsync(int reservationId, int userId);
        Task<bool> AreToolsAvailableAsync(List<int> toolIds, DateTime startDate, DateTime endDate, int? excludeReservationId = null);

        // Admin functions
        Task<List<ReservationSummaryDTO>> GetActiveReservationsAsync();
    }
}
