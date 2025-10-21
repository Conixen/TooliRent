//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AutoMapper;
//using TooliRent.Core.Interfaces.IRepository;
//using TooliRent.Core.Interfaces.IService;
//using TooliRent.DTO_s.ReservationDTOs;
//using TooliRent.Models;

//namespace TooliRent.Services.Service
//{
//    public class ReservationService : IReservationService
//    {
//        private readonly IReservationRepository _reservationRepository;
//        private readonly IToolRepository _toolRepository;
//        private readonly IMapper _mapper;
//        public ReservationService(IReservationRepository reservationRepository, IToolRepository toolRepository, IMapper mapper)
//        {
//            _reservationRepository = reservationRepository;
//            _toolRepository = toolRepository;
//            _mapper = mapper;
//        }
//        public async Task<ReservationDetailDTO?> GetByIdAsync(int id)
//        {
//            var reservation = await _reservationRepository.GetByIdAsync(id);

//            if (reservation == null)
//                return null;

//            var dto = _mapper.Map<ReservationDetailDTO>(reservation);

//            // Calculate business logic fields
//            dto.CanBeCancelled = CanBeCancelled(reservation);
//            dto.CanBeModified = CanBeModified(reservation);

//            return dto;
//        }

//        public async Task<List<ReservationSummaryDTO>> GetAllAsync()
//        {
//            var reservations = await _reservationRepository.GetAllAsync();
//            return _mapper.Map<List<ReservationSummaryDTO>>(reservations);
//        }

//        public async Task<List<ReservationSummaryDTO>> GetByUserIdAsync(int userId)
//        {
//            var reservations = await _reservationRepository.GetByUserIdAsync(userId);
//            return _mapper.Map<List<ReservationSummaryDTO>>(reservations);
//        }

//        public async Task<ReservationDetailDTO> CreateAsync(CreateReservationDTO dto, int userId)
//        {
//            // Validate dates
//            if (dto.Date2Return <= dto.Date2Hire)
//                throw new ArgumentException("Return date must be after hire date");

//            if (dto.Date2Hire < DateTime.UtcNow.Date)
//                throw new ArgumentException("Hire date cannot be in the past");

//            // Check tool availability
//            if (!await AreToolsAvailableAsync(dto.ToolIds, dto.Date2Hire, dto.Date2Return))
//                throw new InvalidOperationException("One or more selected tools are not available for the specified dates");

//            // Create reservation
//            var reservation = _mapper.Map<Reservation>(dto);
//            reservation.UserId = userId;
//            reservation.CreatedAt = DateTime.UtcNow;
//            reservation.UpdatedAt = DateTime.UtcNow;

//            var createdReservation = await _reservationRepository.AddAsync(reservation);

//            // Create ReservationTools
//            var durationDays = (dto.Date2Return - dto.Date2Hire).Days + 1;

//            foreach (var toolId in dto.ToolIds)
//            {
//                var tool = await _toolRepository.GetByIdAsync(toolId);
//                if (tool != null)
//                {
//                    var reservationTool = new ReservationTool
//                    {
//                        ReservationId = createdReservation.Id,
//                        ToolId = toolId,
//                        EstimatedDays = durationDays,
//                        EstimatedPrice = tool.PricePerDay * durationDays,
//                        Notes = dto.Notes
//                    };

//                    await _reservationRepository.AddReservationToolAsync(reservationTool);
//                }
//            }

//            // Get complete reservation with all relationships
//            var fullReservation = await _reservationRepository.GetByIdAsync(createdReservation.Id);
//            var result = _mapper.Map<ReservationDetailDTO>(fullReservation);
//            result.CanBeCancelled = CanBeCancelled(fullReservation);
//            result.CanBeModified = CanBeModified(fullReservation);

//            return result;
//        }

//        public async Task UpdateAsync(int id, UpdateReservationDTO dto)
//        {
//            var existingReservation = await _reservationRepository.GetByIdAsync(id);

//            if (existingReservation == null)
//                throw new ArgumentException("Reservation not found");

//            if (!CanBeModified(existingReservation))
//                throw new InvalidOperationException("This reservation cannot be modified");

//            // Validate dates
//            if (dto.Date2Return <= dto.Date2Hire)
//                throw new ArgumentException("Return date must be after hire date");

//            // Check tool availability (exclude current reservation)
//            if (!await AreToolsAvailableAsync(dto.ToolIds, dto.Date2Hire, dto.Date2Return, id))
//                throw new InvalidOperationException("One or more selected tools are not available for the specified dates");

//            // Update reservation
//            _mapper.Map(dto, existingReservation);
//            existingReservation.UpdatedAt = DateTime.UtcNow;

//            await _reservationRepository.UpdateAsync(existingReservation);

//            // Update ReservationTools
//            await _reservationRepository.ClearReservationToolsAsync(id);

//            var durationDays = (dto.Date2Return - dto.Date2Hire).Days + 1;

//            foreach (var toolId in dto.ToolIds)
//            {
//                var tool = await _toolRepository.GetByIdAsync(toolId);
//                if (tool != null)
//                {
//                    var reservationTool = new ReservationTool
//                    {
//                        ReservationId = id,
//                        ToolId = toolId,
//                        EstimatedDays = durationDays,
//                        EstimatedPrice = tool.PricePerDay * durationDays,
//                        Notes = dto.Notes
//                    };

//                    await _reservationRepository.AddReservationToolAsync(reservationTool);
//                }
//            }
//        }

//        public async Task CancelAsync(int id, ReservationCancelDTO dto)
//        {
//            var reservation = await _reservationRepository.GetByIdAsync(id);

//            if (reservation == null)
//                throw new ArgumentException("Reservation not found");

//            if (!CanBeCancelled(reservation))
//                throw new InvalidOperationException("This reservation cannot be cancelled");

//            reservation.Status = "Cancelled";
//            reservation.CanceledAt = DateTime.UtcNow;
//            reservation.CanceledReason = dto.Reason;
//            reservation.UpdatedAt = DateTime.UtcNow;

//            await _reservationRepository.UpdateAsync(reservation);
//        }

//        public async Task<bool> CanUserCancelAsync(int reservationId, int userId)
//        {
//            var reservation = await _reservationRepository.GetByIdAsync(reservationId);
//            return reservation != null && reservation.UserId == userId && CanBeCancelled(reservation);
//        }

//        public async Task<bool> CanUserModifyAsync(int reservationId, int userId)
//        {
//            var reservation = await _reservationRepository.GetByIdAsync(reservationId);
//            return reservation != null && reservation.UserId == userId && CanBeModified(reservation);
//        }

//        public async Task<bool> AreToolsAvailableAsync(List<int> toolIds, DateTime startDate, DateTime endDate, int? excludeReservationId = null)
//        {
//            return await _reservationRepository.AreToolsAvailableAsync(toolIds, startDate, endDate, excludeReservationId);
//        }

//        public async Task<List<ReservationSummaryDTO>> GetActiveReservationsAsync()
//        {
//            var reservations = await _reservationRepository.GetActiveReservationsAsync();
//            return _mapper.Map<List<ReservationSummaryDTO>>(reservations);
//        }

//        public async Task DeleteAsync(int id)
//        {
//            var reservation = await _reservationRepository.GetByIdAsync(id);

//            if (reservation == null)
//                throw new ArgumentException("Reservation not found");

//            await _reservationRepository.DeleteAsync(id);
//        }

//        // Private helper methods
//        private static bool CanBeCancelled(Reservation reservation)
//        {
//            return reservation.Status == "Active" &&
//                   reservation.Date2Hire > DateTime.UtcNow.Date;
//        }

//        private static bool CanBeModified(Reservation reservation)
//        {
//            return reservation.Status == "Active" &&
//                   reservation.Date2Hire > DateTime.UtcNow.Date.AddDays(1); // At least 1 day in advance
//        }
//    }
//}

