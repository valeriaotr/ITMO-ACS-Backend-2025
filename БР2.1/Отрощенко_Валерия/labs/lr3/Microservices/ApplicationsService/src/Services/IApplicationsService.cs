using ApplicationsService.DTOs;
using ApplicationsService.Models;

namespace ApplicationsService.Services;

public interface IApplicationsService
{
    Task<JobApplication> CreateAsync(CreateApplicationDto dto, CancellationToken ct);
    Task<JobApplication> GetAsync(Guid id, CancellationToken ct);
}