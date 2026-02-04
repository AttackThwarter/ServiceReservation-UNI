using ServiceReservation.Models;
using ServiceReservation.Services.Interfaces;
using ServiceReservation.UnitOfWork;

namespace ServiceReservation.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiceManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await _unitOfWork.Services.GetAllAsync();
        }

        public async Task<IEnumerable<Service>> GetActiveServicesAsync()
        {
            return await _unitOfWork.Services.GetActiveServicesAsync();
        }

        public async Task<Service?> GetServiceByIdAsync(int id)
        {
            return await _unitOfWork.Services.GetByIdAsync(id);
        }

        public async Task<Service?> GetServiceWithTimeSlotsAsync(int id)
        {
            return await _unitOfWork.Services.GetServiceWithTimeSlotsAsync(id);
        }

        public async Task CreateServiceAsync(Service service)
        {
            await _unitOfWork.Services.AddAsync(service);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateServiceAsync(Service service)
        {
            _unitOfWork.Services.Update(service);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteServiceAsync(int id)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(id);
            if (service != null)
            {
                _unitOfWork.Services.Remove(service);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task ToggleServiceStatusAsync(int id)
        {
            var service = await _unitOfWork.Services.GetByIdAsync(id);
            if (service != null)
            {
                service.IsActive = !service.IsActive;
                _unitOfWork.Services.Update(service);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
