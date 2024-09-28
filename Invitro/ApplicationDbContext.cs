using Invitro.Models;
using Microsoft.EntityFrameworkCore;

namespace Invitro;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Procedure> Procedures { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Child> Children { get; set; }
    public DbSet<Analysis> Analyses { get; set; }
    
    
    
}
