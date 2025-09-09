using ApplicationsService.Data;
using ApplicationsService.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsService.Repository;

public class ApplicationsRepository(AppDbContext db) : IApplicationsRepository
{
    public async Task AddAsync(JobApplication a, CancellationToken ct) => await db.Applications.AddAsync(a, ct);
    public Task<JobApplication?> GetAsync(Guid id, CancellationToken ct) => db.Applications.FirstOrDefaultAsync(x => x.Id == id, ct);
    public Task SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);
}