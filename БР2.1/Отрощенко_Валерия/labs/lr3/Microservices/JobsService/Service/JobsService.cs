using JobsService.Clients;
using JobsService.DTOs;
using JobsService.Repository;
using Microsoft.EntityFrameworkCore;

namespace JobsService.Service;

public class JobsService(IJobRepository repo, IProfilesClient profiles, ISearchClient search) : IJobsService
{
    public async Task<JobVacancy> CreateAsync(CreateJobDto dto, CancellationToken ct)
    {
        if (!await profiles.EmployerExistsAsync(dto.EmployerId, ct))
            throw new InvalidOperationException("Employer not found");

        var j = new JobVacancy
        {
            Id = Guid.NewGuid(),
            EmployerId = dto.EmployerId,
            Title = dto.Title,
            Description = dto.Description,
            Requirements = dto.Requirements,
            SalaryMin = dto.SalaryMin,
            SalaryMax = dto.SalaryMax,
            ExperienceRequired = dto.ExperienceRequired,
            Industry = dto.Industry
        };
        await repo.AddAsync(j, ct);
        await repo.SaveChangesAsync(ct);
        
        for (var attempt = 1; attempt <= 3; attempt++)
        {
            try { await search.UpsertAsync(j, ct); break; }
            catch when (attempt < 3) { await Task.Delay(200 * attempt, ct); }
        }
        return j;
    }

    public async Task<JobVacancy> GetAsync(Guid id, CancellationToken ct) =>
        await repo.GetAsync(id, ct) ?? throw new KeyNotFoundException("Job not found");

    public async Task<IEnumerable<JobVacancy>> SearchAsync(string? q, string? industry, int? minSalary, int? maxSalary, int? minExp, CancellationToken ct)
    {
        var query = repo.Query().Where(j => j.IsActive);

        if (!string.IsNullOrWhiteSpace(q))
        {
            var qq = q.ToLower();
            query = query.Where(j => j.Title.ToLower().Contains(qq) || j.Description.ToLower().Contains(qq));
        }
        if (!string.IsNullOrWhiteSpace(industry)) query = query.Where(j => j.Industry == industry);
        if (minSalary is { } s1) query = query.Where(j => j.SalaryMax >= s1);
        if (maxSalary is { } s2) query = query.Where(j => j.SalaryMin <= s2);
        if (minExp   is { } e ) query = query.Where(j => j.ExperienceRequired >= e);

        return await query.OrderByDescending(j => j.CreatedAt).Take(100).ToListAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var j = await repo.GetAsync(id, ct) ?? throw new KeyNotFoundException("Job not found");
        await repo.RemoveAsync(j, ct);
        await repo.SaveChangesAsync(ct);
        try { await search.DeleteAsync(id, ct); } catch { /* ignore */ }
    }
}