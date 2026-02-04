using ServiceReservation.Repositories.Interfaces;

namespace ServiceReservation.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IServiceRepository Services { get; }
        ITimeSlotRepository TimeSlots { get; }
        IBookingRepository Bookings { get; }
        IAuditLogRepository AuditLogs { get; }
        
        Task<int> SaveAsync();
    }
}
