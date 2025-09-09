namespace ProfilesService.DTOs;

public record CreateApplicantDto(Guid UserId, string FullName, string ResumeText, int ExperienceYears, int ExpectedSalary, string Specialization);
public record CreateEmployerDto(Guid UserId, string CompanyName, string CompanyDescription, string Industry);