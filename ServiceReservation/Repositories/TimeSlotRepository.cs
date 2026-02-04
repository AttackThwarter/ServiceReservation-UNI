using Microsoft.EntityFrameworkCore;
using ServiceReservation.Data;
using ServiceReservation.Models;
using ServiceReservation.Repositories.Interfaces;

namespace ServiceReservation.Repositories
{
    public class TimeSlotRepository : GenericRepository<TimeSlot>, ITimeSlotRepository
    {
        public TimeSlotRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TimeSlot>> GetAvailableSlotsByServiceAsync(int serviceId)
        {
            return await _dbSet
                .Where(t => t.ServiceId == serviceId && t.StartTime > DateTime.Now)
                .Include(t => t.Bookings)
                .Where(t => t.Bookings.Count(b => b.Status == BookingStatus.Approved) < t.Capacity)
                .OrderBy(t => t.StartTime)
                .ToListAsync();
        }

        public async Task<TimeSlot?> GetSlotWithBookingsAsync(int slotId)
        {
            return await _dbSet
                .Include(t => t.Bookings)
                    .ThenInclude(b => b.User)
                .Include(t => t.Service)
                .FirstOrDefaultAsync(t => t.Id == slotId);
        }

        public async Task<bool> HasAvailableCapacityAsync(int slotId)
        {
            var slot = await _dbSet
                .Include(t => t.Bookings)
                .FirstOrDefaultAsync(t => t.Id == slotId);

            if (slot == null) return false;

            var approvedCount = slot.Bookings.Count(b => b.Status == BookingStatus.Approved);
            return approvedCount < slot.Capacity;
        }
    }
}
