using Microsoft.EntityFrameworkCore;

namespace HospitalWebsiteApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserParameters> UserParameters { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor_Roles> Doctor_Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



        }
    }
}
