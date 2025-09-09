using ProfilesService.Models;

namespace ProfilesService.Service;

public interface IProfilesService
{
    Task<ApplicantProfile> CreateApplicantAsync(ApplicantProfile a, CancellationToken ct);
    Task<ApplicantProfile> GetApplicantAsync(Guid id, CancellationToken ct);
    Task<EmployerProfile> CreateEmployerAsync(EmployerProfile e, CancellationToken ct);
    Task<EmployerProfile> GetEmployerAsync(Guid id, CancellationToken ct);
}