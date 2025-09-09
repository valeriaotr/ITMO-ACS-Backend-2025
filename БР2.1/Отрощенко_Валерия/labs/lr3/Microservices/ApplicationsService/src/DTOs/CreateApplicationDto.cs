namespace ApplicationsService.DTOs;

public record CreateApplicationDto(Guid ApplicantId, Guid VacancyId, string CoverLetter);