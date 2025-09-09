namespace ApplicationsService.Clients;

public interface IJobsClient
{
    Task<bool> JobExistsAsync(Guid jobId, CancellationToken ct);
}