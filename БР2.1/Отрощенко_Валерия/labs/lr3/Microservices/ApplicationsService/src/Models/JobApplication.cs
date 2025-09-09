namespace ApplicationsService.Models;

public class JobApplication
{
    public Guid Id { get; set; }
    public Guid ApplicantId { get; set; }
    public Guid VacancyId { get; set; }
    public string CoverLetter { get; set; } = string.Empty;
    public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
}