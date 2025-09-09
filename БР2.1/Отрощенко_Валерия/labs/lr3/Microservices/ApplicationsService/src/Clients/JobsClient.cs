namespace ApplicationsService.Clients;

public class JobsClient(HttpClient http) : IJobsClient
{
    public async Task<bool> JobExistsAsync(Guid id, CancellationToken ct)
    {
        using var resp = await http.GetAsync($"/jobs/{id}", ct);
        return resp.IsSuccessStatusCode;
    }
}