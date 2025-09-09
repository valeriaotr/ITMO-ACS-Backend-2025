using ProfilesService.Models;

namespace ProfilesService.Repository;

public interface IApplicantRepository
{
    Task<ApplicantProfile?> GetAsync(Guid id, CancellationToken ct);
    Task AddAsync(ApplicantProfile a, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}