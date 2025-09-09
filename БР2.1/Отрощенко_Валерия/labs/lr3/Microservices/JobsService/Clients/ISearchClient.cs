namespace JobsService.Clients;

public interface ISearchClient
{
    Task UpsertAsync(JobVacancy job, CancellationToken ct);
    Task DeleteAsync(Guid jobId, CancellationToken ct);
}