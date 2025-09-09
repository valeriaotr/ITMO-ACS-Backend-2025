using ProfilesService.Models;
using ProfilesService.Repository;

namespace ProfilesService.Service;

public class ProfilesService(IApplicantRepository ar, IEmployerRepository er) : IProfilesService
{
    public async Task<ApplicantProfile> CreateApplicantAsync(ApplicantProfile a, CancellationToken ct)
    {
        a.Id = Guid.NewGuid();                     
        await ar.AddAsync(a, ct);
        await ar.SaveChangesAsync(ct);
        return a;
    }

    public async Task<ApplicantProfile> GetApplicantAsync(Guid id, CancellationToken ct) =>
        await ar.GetAsync(id, ct) ?? throw new KeyNotFoundException("Applicant not found");

    public async Task<EmployerProfile> CreateEmployerAsync(EmployerProfile e, CancellationToken ct)
    {
        e.Id = Guid.NewGuid();
        await er.AddAsync(e, ct);
        await er.SaveChangesAsync(ct);
        return e;
    }

    public async Task<EmployerProfile> GetEmployerAsync(Guid id, CancellationToken ct) =>
        await er.GetAsync(id, ct) ?? throw new KeyNotFoundException("Employer not found");
}