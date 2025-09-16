using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.Core.Interfaces.IRepository;
using TooliRent.Models;
using TooliRent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TooliRent.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly TooliRentContext _context;
        public ReservationRepository(TooliRentContext context)
        {
            _context = context;
        }
        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservations.FindAsync(id);
        }
        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _context.Reservations.ToListAsync();
        }
        public async Task<Reservation> AddAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }
        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
        // ---------------------------------------------------------------------------
        public async Task AddReservationToolAsync(ReservationTool reservationTool)
        {
            _context.ReservationTools.Add(reservationTool);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveReservationToolAsync(int reservationId, int toolId)
        {
            var reservationTool = await _context.ReservationTools
                .FirstOrDefaultAsync(rt => rt.ReservationId == reservationId && rt.ToolId == toolId);
            if (reservationTool != null)
            {
                _context.ReservationTools.Remove(reservationTool);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<ReservationTool>> GetReservationToolsAsync(int reservationId)
        {
            return await _context.ReservationTools
                .Where(rt => rt.ReservationId == reservationId)
                .ToListAsync();
        }
    }
}
