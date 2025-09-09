using ApplicationsService.Clients;
using ApplicationsService.DTOs;
using ApplicationsService.Models;
using ApplicationsService.Repository;

namespace ApplicationsService.Services;

public class ApplicationsService(IApplicationsRepository repo, IProfilesClient profiles, IJobsClient jobs) : IApplicationsService
{
    public async Task<JobApplication> CreateAsync(CreateApplicationDto dto, CancellationToken ct)
    {
        if (!await profiles.ApplicantExistsAsync(dto.ApplicantId, ct))
            throw new InvalidOperationException("Applicant not found");
        if (!await jobs.JobExistsAsync(dto.VacancyId, ct))
            throw new InvalidOperationException("Job not found");

        var a = new JobApplication { Id = Guid.NewGuid(), ApplicantId = dto.ApplicantId, VacancyId = dto.VacancyId, CoverLetter = dto.CoverLetter };
        await repo.AddAsync(a, ct);
        await repo.SaveChangesAsync(ct);
        return a;
    }

    public async Task<JobApplication> GetAsync(Guid id, CancellationToken ct) =>
        await repo.GetAsync(id, ct) ?? throw new KeyNotFoundException("Application not found");
}