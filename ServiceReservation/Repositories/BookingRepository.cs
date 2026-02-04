using Microsoft.EntityFrameworkCore;
using ServiceReservation.Data;
using ServiceReservation.Models;
using ServiceReservation.Repositories.Interfaces;

namespace ServiceReservation.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId)
        {
            return await _dbSet
                .Where(b => b.UserId == userId)
                .Include(b => b.Service)
                .Include(b => b.TimeSlot)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByStatusAsync(BookingStatus status)
        {
            return await _dbSet
                .Where(b => b.Status == status)
                .Include(b => b.User)
                .Include(b => b.Service)
                .Include(b => b.TimeSlot)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByServiceAsync(int serviceId)
        {
            return await _dbSet
                .Where(b => b.ServiceId == serviceId)
                .Include(b => b.User)
                .Include(b => b.TimeSlot)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingWithDetailsAsync(int bookingId)
        {
            return await _dbSet
                .Include(b => b.User)
                .Include(b => b.Service)
                .Include(b => b.TimeSlot)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }

        public async Task<bool> HasUserBookedSlotAsync(string userId, int timeSlotId)
        {
            return await _dbSet.AnyAsync(b =>
                b.UserId == userId &&
                b.TimeSlotId == timeSlotId &&
                b.Status != BookingStatus.Cancelled);
        }

        public async Task<int> GetBookingsCountBySlotAsync(int timeSlotId)
        {
            return await _dbSet.CountAsync(b =>
                b.TimeSlotId == timeSlotId &&
                b.Status == BookingStatus.Approved);
        }

        public async Task<IEnumerable<Booking>> GetPendingBookingsAsync()
        {
            return await _dbSet
                .Where(b => b.Status == BookingStatus.Pending)
                .Include(b => b.User)
                .Include(b => b.Service)
                .Include(b => b.TimeSlot)
                .OrderBy(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetTodayBookingsAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            return await _dbSet
                .Include(b => b.TimeSlot)
                .Where(b => b.TimeSlot!.StartTime >= today && b.TimeSlot.StartTime < tomorrow)
                .Include(b => b.User)
                .Include(b => b.Service)
                .OrderBy(b => b.TimeSlot!.StartTime)
                .ToListAsync();
        }
    }
}
