using Microsoft.EntityFrameworkCore;
using ProfilesService.Models;

namespace ProfilesService.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ApplicantProfile> Applicants => Set<ApplicantProfile>();
    public DbSet<EmployerProfile> Employers => Set<EmployerProfile>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.HasPostgresExtension("uuid-ossp");

        b.Entity<ApplicantProfile>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
            e.Property(x => x.FullName).IsRequired().HasMaxLength(200);
            e.Property(x => x.Specialization).HasMaxLength(100);
        });

        b.Entity<EmployerProfile>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
            e.Property(x => x.CompanyName).IsRequired().HasMaxLength(200);
            e.Property(x => x.Industry).HasMaxLength(100);
        });
    }
}