using Microsoft.AspNetCore.Mvc;
using ProfilesService.DTOs;
using ProfilesService.Models;
using ProfilesService.Service;

namespace ProfilesService.Controllers;

[ApiController]
[Route("applicants")]
public class ApplicantsController(IProfilesService svc) : ControllerBase
{
    [HttpPost("applicant")]
    public async Task<ActionResult<ApplicantProfile>> CreateApplicant(
        [FromBody] CreateApplicantDto dto,
        CancellationToken ct)
    {
        var profile = new ApplicantProfile
        {
            Id = Guid.NewGuid(),   // генерим на сервисе
            UserId = dto.UserId,
            FullName = dto.FullName,
            ResumeText = dto.ResumeText,
            ExperienceYears = dto.ExperienceYears,
            ExpectedSalary = dto.ExpectedSalary,
            Specialization = dto.Specialization,
        };

        var created = await svc.CreateApplicantAsync(profile, ct);
        return CreatedAtAction(nameof(GetApplicant), new { id = created.Id }, created);
    }

    [HttpGet("applicant/{id:guid}")]
    public async Task<ActionResult<ApplicantProfile>> GetApplicant(Guid id, CancellationToken ct) =>
        (ActionResult<ApplicantProfile>)await svc.GetApplicantAsync(id, ct) ?? NotFound();
}