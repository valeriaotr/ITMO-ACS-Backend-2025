namespace JobsService.Clients;

public class ProfilesClient(HttpClient http) : IProfilesClient
{
    public async Task<bool> EmployerExistsAsync(Guid employerId, CancellationToken ct)
    {
        using var resp = await http.GetAsync($"/employers/employer/{employerId}", ct);
        return resp.IsSuccessStatusCode;
    }
}