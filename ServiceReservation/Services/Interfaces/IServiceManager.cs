using ServiceReservation.Models;

namespace ServiceReservation.Services.Interfaces
{
    public interface IServiceManager
    {
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<IEnumerable<Service>> GetActiveServicesAsync();
        Task<Service?> GetServiceByIdAsync(int id);
        Task<Service?> GetServiceWithTimeSlotsAsync(int id);
        Task CreateServiceAsync(Service service);
        Task UpdateServiceAsync(Service service);
        Task DeleteServiceAsync(int id);
        Task ToggleServiceStatusAsync(int id);
    }
}
