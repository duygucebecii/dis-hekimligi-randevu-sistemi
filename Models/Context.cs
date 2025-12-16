using Microsoft.EntityFrameworkCore;

namespace randevu_sistemi.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MONSTER\\SQLEXPRESS01;Database=DisKlinigi;" +
                "User ID=sa;Password=1;Trusted_Connection=False;TrustServerCertificate=True");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Dentist> Dentists { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
