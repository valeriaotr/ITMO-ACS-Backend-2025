namespace ApplicationsService.Clients;

public class ProfilesClient(HttpClient http) : IProfilesClient
{
    public async Task<bool> ApplicantExistsAsync(Guid id, CancellationToken ct)
    {
        using var resp = await http.GetAsync($"/applicants/applicant/{id}", ct);
        return resp.IsSuccessStatusCode;
    }
}