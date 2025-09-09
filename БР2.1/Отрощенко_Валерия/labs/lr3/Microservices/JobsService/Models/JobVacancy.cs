public class JobVacancy
{
    public Guid Id { get; set; }
    public Guid EmployerId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public int SalaryMin { get; set; }
    public int SalaryMax { get; set; }
    public int ExperienceRequired { get; set; }
    public string Industry { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}