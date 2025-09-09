namespace JobsService.Clients;

public class SearchClient(HttpClient http) : ISearchClient
{
    public Task UpsertAsync(JobVacancy j, CancellationToken ct) =>
        http.PostAsJsonAsync("/index/jobs", new {
            Id = j.Id, j.EmployerId, j.Title, j.Description, j.Requirements,
            j.SalaryMin, j.SalaryMax, j.ExperienceRequired, j.Industry
        }, ct);

    public Task DeleteAsync(Guid jobId, CancellationToken ct) =>
        http.DeleteAsync($"/index/jobs/{jobId}", ct);
}