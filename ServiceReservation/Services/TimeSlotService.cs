using ServiceReservation.Models;
using ServiceReservation.Services.Interfaces;
using ServiceReservation.UnitOfWork;

namespace ServiceReservation.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TimeSlotService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TimeSlot>> GetSlotsByServiceAsync(int serviceId)
        {
            return await _unitOfWork.TimeSlots.FindAsync(t => t.ServiceId == serviceId);
        }

        public async Task<IEnumerable<TimeSlot>> GetAvailableSlotsAsync(int serviceId)
        {
            return await _unitOfWork.TimeSlots.GetAvailableSlotsByServiceAsync(serviceId);
        }

        public async Task<TimeSlot?> GetSlotByIdAsync(int id)
        {
            return await _unitOfWork.TimeSlots.GetByIdAsync(id);
        }

        public async Task<TimeSlot?> GetSlotWithBookingsAsync(int id)
        {
            return await _unitOfWork.TimeSlots.GetSlotWithBookingsAsync(id);
        }

        public async Task CreateTimeSlotAsync(TimeSlot slot)
        {
            await _unitOfWork.TimeSlots.AddAsync(slot);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateTimeSlotAsync(TimeSlot slot)
        {
            _unitOfWork.TimeSlots.Update(slot);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteTimeSlotAsync(int id)
        {
            var slot = await _unitOfWork.TimeSlots.GetByIdAsync(id);
            if (slot != null)
            {
                _unitOfWork.TimeSlots.Remove(slot);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<bool> HasAvailableCapacityAsync(int slotId)
        {
            return await _unitOfWork.TimeSlots.HasAvailableCapacityAsync(slotId);
        }
    }
}
