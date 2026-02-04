using ServiceReservation.Models;

namespace ServiceReservation.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<IEnumerable<Booking>> GetPendingBookingsAsync();
        Task<IEnumerable<Booking>> GetTodayBookingsAsync();
        Task<IEnumerable<Booking>> GetBookingsByServiceAsync(int serviceId);
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<Booking?> GetBookingWithDetailsAsync(int id);
        Task<(bool Success, string Message)> CreateBookingAsync(Booking booking);
        Task<bool> ApproveBookingAsync(int bookingId);
        Task<bool> RejectBookingAsync(int bookingId);
        Task<bool> CancelBookingAsync(int bookingId, string userId);
        Task<bool> HasUserBookedSlotAsync(string userId, int timeSlotId);
    }
}
