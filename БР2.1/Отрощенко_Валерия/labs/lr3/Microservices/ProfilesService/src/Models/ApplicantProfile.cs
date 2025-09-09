namespace ProfilesService.Models;

public class ApplicantProfile
{
    public Guid Id { get; set; } 
    public Guid UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string ResumeText { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    public int ExpectedSalary { get; set; }
    public string Specialization { get; set; } = string.Empty;
}