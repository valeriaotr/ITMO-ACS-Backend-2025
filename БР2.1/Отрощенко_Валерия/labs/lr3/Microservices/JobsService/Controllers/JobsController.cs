using JobsService.DTOs;
using JobsService.Service;
using Microsoft.AspNetCore.Mvc;

namespace JobsService.Controllers;

[ApiController]
[Route("jobs")]
public class JobsController(IJobsService svc) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJobDto dto, CancellationToken ct)
    {
        var j = await svc.CreateAsync(dto, ct);
        return Created($"/jobs/{j.Id}", j);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct) =>
        Ok(await svc.GetAsync(id, ct));

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] string? q, [FromQuery] string? industry,
        [FromQuery] int? minSalary, [FromQuery] int? maxSalary, [FromQuery] int? minExp, CancellationToken ct) =>
        Ok(await svc.SearchAsync(q, industry, minSalary, maxSalary, minExp, ct));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await svc.DeleteAsync(id, ct);
        return NoContent();
    }
}