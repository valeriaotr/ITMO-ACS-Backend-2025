using Microsoft.EntityFrameworkCore;
using ProfilesService.Data;
using ProfilesService.Models;

namespace ProfilesService.Repository;

public class EmployerRepository(AppDbContext db) : IEmployerRepository
{
    public Task<EmployerProfile?> GetAsync(Guid id, CancellationToken ct) =>
        db.Employers.FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task AddAsync(EmployerProfile e, CancellationToken ct) =>
        await db.Employers.AddAsync(e, ct);

    public Task SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);
}