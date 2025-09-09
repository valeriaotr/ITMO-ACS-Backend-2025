namespace ProfilesService.Models;

public class EmployerProfile
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyDescription { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
}