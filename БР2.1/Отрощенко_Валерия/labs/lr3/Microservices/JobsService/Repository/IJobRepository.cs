namespace JobsService.Repository;

public interface IJobRepository
{
    Task AddAsync(JobVacancy job, CancellationToken ct);
    Task<JobVacancy?> GetAsync(Guid id, CancellationToken ct);
    IQueryable<JobVacancy> Query();
    Task RemoveAsync(JobVacancy job, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}