using ProfilesService.Models;

namespace ProfilesService.Repository;

public interface IEmployerRepository
{
    Task<EmployerProfile?> GetAsync(Guid id, CancellationToken ct);
    Task AddAsync(EmployerProfile e, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}