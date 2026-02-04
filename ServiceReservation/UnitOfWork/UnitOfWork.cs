using ServiceReservation.Data;
using ServiceReservation.Repositories;
using ServiceReservation.Repositories.Interfaces;

namespace ServiceReservation.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        
        private IServiceRepository? _services;
        private ITimeSlotRepository? _timeSlots;
        private IBookingRepository? _bookings;
        private IAuditLogRepository? _auditLogs;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IServiceRepository Services => 
            _services ??= new ServiceRepository(_context);

        public ITimeSlotRepository TimeSlots => 
            _timeSlots ??= new TimeSlotRepository(_context);

        public IBookingRepository Bookings => 
            _bookings ??= new BookingRepository(_context);

        public IAuditLogRepository AuditLogs => 
            _auditLogs ??= new AuditLogRepository(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
