using Microsoft.EntityFrameworkCore;
using ServiceReservation.Data;
using ServiceReservation.Models;
using ServiceReservation.Repositories.Interfaces;

namespace ServiceReservation.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Service>> GetActiveServicesAsync()
        {
            return await _dbSet
                .Where(s => s.IsActive)
                .OrderBy(s => s.Title)
                .ToListAsync();
        }

        public async Task<Service?> GetServiceWithTimeSlotsAsync(int serviceId)
        {
            return await _dbSet
                .Include(s => s.TimeSlots)
                .FirstOrDefaultAsync(s => s.Id == serviceId);
        }
    }
}
