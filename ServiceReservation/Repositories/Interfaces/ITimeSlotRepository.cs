using ServiceReservation.Models;

namespace ServiceReservation.Repositories.Interfaces
{
    public interface ITimeSlotRepository : IGenericRepository<TimeSlot>
    {
        Task<IEnumerable<TimeSlot>> GetAvailableSlotsByServiceAsync(int serviceId);
        Task<TimeSlot?> GetSlotWithBookingsAsync(int slotId);
        Task<bool> HasAvailableCapacityAsync(int slotId);
    }
}
