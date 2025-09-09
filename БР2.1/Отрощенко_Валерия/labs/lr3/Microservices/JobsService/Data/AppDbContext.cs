using Microsoft.EntityFrameworkCore;

namespace JobsService.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<JobVacancy> Jobs => Set<JobVacancy>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.HasPostgresExtension("uuid-ossp");
        b.Entity<JobVacancy>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
            e.Property(x => x.Title).IsRequired().HasMaxLength(200);
            e.Property(x => x.Industry).HasMaxLength(100);
            e.HasIndex(x => x.CreatedAt);
        });
    }
}