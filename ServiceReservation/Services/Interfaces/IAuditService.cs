using ServiceReservation.Models;

namespace ServiceReservation.Services.Interfaces
{
    public interface IAuditService
    {
        Task LogAsync(string userId, string action, string? ipAddress = null);
        Task<IEnumerable<AuditLog>> GetUserLogsAsync(string userId);
        Task<IEnumerable<AuditLog>> GetRecentLogsAsync(int count = 50);
        Task<IEnumerable<AuditLog>> GetLogsByDateRangeAsync(DateTime start, DateTime end);
    }
}
