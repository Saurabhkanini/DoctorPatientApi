using Microsoft.EntityFrameworkCore;

namespace DoctorPatientsManagementApi.Models
{
    public class DoctorsDbContext:DbContext
    {
        public DoctorsDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<RegisterUser> registerUsers { get; set; }
        public DbSet<Appointment> appointments { get; set; }
    }
}
