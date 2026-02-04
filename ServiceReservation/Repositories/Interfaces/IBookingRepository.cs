using ServiceReservation.Models;

namespace ServiceReservation.Repositories.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId);
        Task<IEnumerable<Booking>> GetBookingsByStatusAsync(BookingStatus status);
        Task<IEnumerable<Booking>> GetBookingsByServiceAsync(int serviceId);
        Task<Booking?> GetBookingWithDetailsAsync(int bookingId);
        Task<bool> HasUserBookedSlotAsync(string userId, int timeSlotId);
        Task<int> GetBookingsCountBySlotAsync(int timeSlotId);
        Task<IEnumerable<Booking>> GetPendingBookingsAsync();
        Task<IEnumerable<Booking>> GetTodayBookingsAsync();
    }
}
