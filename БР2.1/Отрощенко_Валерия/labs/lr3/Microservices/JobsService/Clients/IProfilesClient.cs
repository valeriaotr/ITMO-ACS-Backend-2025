namespace JobsService.Clients;

public interface IProfilesClient
{
    Task<bool> EmployerExistsAsync(Guid employerId, CancellationToken ct);
}