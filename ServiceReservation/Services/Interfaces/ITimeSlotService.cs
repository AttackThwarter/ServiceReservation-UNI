using ServiceReservation.Models;

namespace ServiceReservation.Services.Interfaces
{
    public interface ITimeSlotService
    {
        Task<IEnumerable<TimeSlot>> GetSlotsByServiceAsync(int serviceId);
        Task<IEnumerable<TimeSlot>> GetAvailableSlotsAsync(int serviceId);
        Task<TimeSlot?> GetSlotByIdAsync(int id);
        Task<TimeSlot?> GetSlotWithBookingsAsync(int id);
        Task CreateTimeSlotAsync(TimeSlot slot);
        Task UpdateTimeSlotAsync(TimeSlot slot);
        Task DeleteTimeSlotAsync(int id);
        Task<bool> HasAvailableCapacityAsync(int slotId);
    }
}
