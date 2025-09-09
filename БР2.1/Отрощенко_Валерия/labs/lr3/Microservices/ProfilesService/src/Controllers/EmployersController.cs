using Microsoft.AspNetCore.Mvc;
using ProfilesService.DTOs;
using ProfilesService.Models;
using ProfilesService.Service;

namespace ProfilesService.Controllers;

[ApiController]
[Route("employers")]
public class EmployersController(IProfilesService svc) : ControllerBase
{
    [HttpPost("employer")]
    public async Task<ActionResult<EmployerProfile>> CreateEmployer(
        [FromBody] CreateEmployerDto dto,
        CancellationToken ct)
    {
        var profile = new EmployerProfile
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            CompanyName = dto.CompanyName,
            CompanyDescription = dto.CompanyDescription,
            Industry = dto.Industry,
        };

        var created = await svc.CreateEmployerAsync(profile, ct);
        return CreatedAtAction(nameof(GetEmployer), new { id = created.Id }, created);
    }

    [HttpGet("employer/{id:guid}")]
    public async Task<ActionResult<EmployerProfile>> GetEmployer(Guid id, CancellationToken ct) =>
        (ActionResult<EmployerProfile>)await svc.GetEmployerAsync(id, ct) ?? NotFound();
}