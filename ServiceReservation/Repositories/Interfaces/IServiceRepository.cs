using ServiceReservation.Models;

namespace ServiceReservation.Repositories.Interfaces
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<IEnumerable<Service>> GetActiveServicesAsync();
        Task<Service?> GetServiceWithTimeSlotsAsync(int serviceId);
    }
}
