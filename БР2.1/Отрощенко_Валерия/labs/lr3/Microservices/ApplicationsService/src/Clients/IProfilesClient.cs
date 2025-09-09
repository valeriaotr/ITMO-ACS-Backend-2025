namespace ApplicationsService.Clients;

public interface IProfilesClient
{
    Task<bool> ApplicantExistsAsync(Guid applicantId, CancellationToken ct);
}