using ServiceReservation.Models;
using ServiceReservation.Services.Interfaces;
using ServiceReservation.UnitOfWork;

namespace ServiceReservation.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId)
        {
            return await _unitOfWork.Bookings.GetUserBookingsAsync(userId);
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _unitOfWork.Bookings.GetAllAsync();
        }

        public async Task<IEnumerable<Booking>> GetPendingBookingsAsync()
        {
            return await _unitOfWork.Bookings.GetPendingBookingsAsync();
        }

        public async Task<IEnumerable<Booking>> GetTodayBookingsAsync()
        {
            return await _unitOfWork.Bookings.GetTodayBookingsAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByServiceAsync(int serviceId)
        {
            return await _unitOfWork.Bookings.GetBookingsByServiceAsync(serviceId);
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _unitOfWork.Bookings.GetByIdAsync(id);
        }

        public async Task<Booking?> GetBookingWithDetailsAsync(int id)
        {
            return await _unitOfWork.Bookings.GetBookingWithDetailsAsync(id);
        }

        public async Task<(bool Success, string Message)> CreateBookingAsync(Booking booking)
        {
            // بررسی رزرو تکراری
            var hasBooked = await _unitOfWork.Bookings
                .HasUserBookedSlotAsync(booking.UserId, booking.TimeSlotId);
            
            if (hasBooked)
            {
                return (false, "شما قبلاً این بازه زمانی را رزرو کرده‌اید.");
            }

            // بررسی ظرفیت
            var hasCapacity = await _unitOfWork.TimeSlots
                .HasAvailableCapacityAsync(booking.TimeSlotId);
            
            if (!hasCapacity)
            {
                return (false, "ظرفیت این بازه زمانی تکمیل شده است.");
            }

            booking.Status = BookingStatus.Pending;
            booking.CreatedAt = DateTime.Now;

            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.SaveAsync();

            return (true, "رزرو با موفقیت ثبت شد و در انتظار تأیید است.");
        }

        public async Task<bool> ApproveBookingAsync(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
            if (booking == null || booking.Status != BookingStatus.Pending)
                return false;

            booking.Status = BookingStatus.Approved;
            _unitOfWork.Bookings.Update(booking);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> RejectBookingAsync(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
            if (booking == null || booking.Status != BookingStatus.Pending)
                return false;

            booking.Status = BookingStatus.Rejected;
            _unitOfWork.Bookings.Update(booking);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> CancelBookingAsync(int bookingId, string userId)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
            if (booking == null)
                return false;

            // فقط صاحب رزرو می‌تواند لغو کند
            if (booking.UserId != userId)
                return false;

            // فقط رزروهای در انتظار یا تأیید شده قابل لغو هستند
            if (booking.Status == BookingStatus.Cancelled || 
                booking.Status == BookingStatus.Rejected)
                return false;

            booking.Status = BookingStatus.Cancelled;
            _unitOfWork.Bookings.Update(booking);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> HasUserBookedSlotAsync(string userId, int timeSlotId)
        {
            return await _unitOfWork.Bookings.HasUserBookedSlotAsync(userId, timeSlotId);
        }
    }
}
