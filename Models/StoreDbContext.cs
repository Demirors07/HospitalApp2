using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Models
{
    public class StoreDbContext : DbContext  // DbContext class used for database operations
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }   // Configuration is provided with the DbContextOptions parameter. It is passed to the parent class DbContext with base(options).
        public DbSet<Appointment> Appointments => Set<Appointment>(); // DbSet associated with Appointment table
        public DbSet<Doctor> Doctors => Set<Doctor>(); // DbSet associated with Doctor table
        public DbSet<Clinic> Clinics => Set<Clinic>(); // DbSet associated with the Clinic table
    }
}