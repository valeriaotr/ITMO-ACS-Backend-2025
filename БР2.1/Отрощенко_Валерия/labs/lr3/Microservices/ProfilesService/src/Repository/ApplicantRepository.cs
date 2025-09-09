using Microsoft.EntityFrameworkCore;
using ProfilesService.Data;
using ProfilesService.Models;

namespace ProfilesService.Repository;

public class ApplicantRepository(AppDbContext db) : IApplicantRepository
{
    public Task<ApplicantProfile?> GetAsync(Guid id, CancellationToken ct) =>
        db.Applicants.FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task AddAsync(ApplicantProfile a, CancellationToken ct) =>
        await db.Applicants.AddAsync(a, ct);

    public Task SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);
}