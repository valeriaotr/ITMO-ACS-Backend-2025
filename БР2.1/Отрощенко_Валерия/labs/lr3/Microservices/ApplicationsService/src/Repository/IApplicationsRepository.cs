using ApplicationsService.Models;

namespace ApplicationsService.Repository;

public interface IApplicationsRepository
{
    Task AddAsync(JobApplication a, CancellationToken ct);
    Task<JobApplication?> GetAsync(Guid id, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}