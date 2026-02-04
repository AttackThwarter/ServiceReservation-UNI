using Microsoft.EntityFrameworkCore;
using ServiceReservation.Data;
using ServiceReservation.Models;
using ServiceReservation.Repositories.Interfaces;

namespace ServiceReservation.Repositories
{
    public class AuditLogRepository : GenericRepository<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AuditLog>> GetLogsByUserAsync(string userId)
        {
            return await _context.AuditLogs
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetRecentLogsAsync(int count)
        {
            return await _context.AuditLogs
                .OrderByDescending(a => a.Timestamp)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetLogsByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _context.AuditLogs
                .Where(a => a.Timestamp >= start && a.Timestamp <= end)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }
    }
}
