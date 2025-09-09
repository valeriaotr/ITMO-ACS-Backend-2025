using JobsService.DTOs;

namespace JobsService.Service;

public interface IJobsService
{
    Task<JobVacancy> CreateAsync(CreateJobDto dto, CancellationToken ct);
    Task<JobVacancy> GetAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<JobVacancy>> SearchAsync(string? q, string? industry, int? minSalary, int? maxSalary, int? minExp, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}