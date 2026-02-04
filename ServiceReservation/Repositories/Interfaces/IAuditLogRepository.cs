using ServiceReservation.Models;

namespace ServiceReservation.Repositories.Interfaces
{
    public interface IAuditLogRepository : IGenericRepository<AuditLog>
    {
        Task<IEnumerable<AuditLog>> GetLogsByUserAsync(string userId);
        Task<IEnumerable<AuditLog>> GetRecentLogsAsync(int count);
        Task<IEnumerable<AuditLog>> GetLogsByDateRangeAsync(DateTime start, DateTime end);
    }
}
