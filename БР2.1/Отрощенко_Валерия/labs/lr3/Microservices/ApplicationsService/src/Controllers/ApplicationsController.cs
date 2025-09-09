using ApplicationsService.DTOs;
using ApplicationsService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationsService.Controllers;

[ApiController]
[Route("applications")]
public class ApplicationsController(IApplicationsService svc) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateApplicationDto dto, CancellationToken ct)
    {
        var a = await svc.CreateAsync(dto, ct);
        return Created($"/applications/{a.Id}", a);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct) =>
        Ok(await svc.GetAsync(id, ct));
}