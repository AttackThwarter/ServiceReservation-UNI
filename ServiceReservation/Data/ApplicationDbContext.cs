using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceReservation.Models;

namespace ServiceReservation.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // جلوگیری از رزرو تکراری یک کاربر در یک تایم‌اسلات
            builder.Entity<Booking>()
                .HasIndex(b => new { b.UserId, b.TimeSlotId })
                .IsUnique();

            // Seed Roles با ID های ثابت (GUID های از پیش تعریف شده)
            const string ADMIN_ROLE_ID = "2c5e174e-3b0e-446f-86af-483d56fd7210";
            const string USER_ROLE_ID = "e1e1e1e1-e1e1-e1e1-e1e1-e1e1e1e1e1e1";
            const string OPERATOR_ROLE_ID = "f2f2f2f2-f2f2-f2f2-f2f2-f2f2f2f2f2f2";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = ADMIN_ROLE_ID
                },
                new IdentityRole
                {
                    Id = USER_ROLE_ID,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = USER_ROLE_ID
                },
                new IdentityRole
                {
                    Id = OPERATOR_ROLE_ID,
                    Name = "Operator",
                    NormalizedName = "OPERATOR",
                    ConcurrencyStamp = OPERATOR_ROLE_ID
                }
            );
        }
    }
}
