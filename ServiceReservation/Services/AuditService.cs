using ServiceReservation.Models;
using ServiceReservation.Services.Interfaces;
using ServiceReservation.UnitOfWork;

namespace ServiceReservation.Services
{
    public class AuditService : IAuditService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task LogAsync(string userId, string action, string? ipAddress = null)
        {
            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                Timestamp = DateTime.Now,
                IPAddress = ipAddress
            };

            await _unitOfWork.AuditLogs.AddAsync(log);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetUserLogsAsync(string userId)
        {
            return await _unitOfWork.AuditLogs.GetLogsByUserAsync(userId);
        }

        public async Task<IEnumerable<AuditLog>> GetRecentLogsAsync(int count = 50)
        {
            return await _unitOfWork.AuditLogs.GetRecentLogsAsync(count);
        }

        public async Task<IEnumerable<AuditLog>> GetLogsByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _unitOfWork.AuditLogs.GetLogsByDateRangeAsync(start, end);
        }
    }
}
