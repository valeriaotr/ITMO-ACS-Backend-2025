using JobsService.Data;
using Microsoft.EntityFrameworkCore;

namespace JobsService.Repository;

public class JobRepository(AppDbContext db) : IJobRepository
{
    public async Task AddAsync(JobVacancy job, CancellationToken ct) => await db.Jobs.AddAsync(job, ct);
    public Task<JobVacancy?> GetAsync(Guid id, CancellationToken ct) => db.Jobs.FirstOrDefaultAsync(x => x.Id == id, ct);
    public IQueryable<JobVacancy> Query() => db.Jobs.AsQueryable();
    public Task RemoveAsync(JobVacancy job, CancellationToken ct) { db.Jobs.Remove(job); return Task.CompletedTask; }
    public Task SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);
}