using System.Security.Claims;
using IdentityService.DTOs;
using IdentityService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controller;

[ApiController]
[Route("")]
public class AuthController(IAuthService auth) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req, CancellationToken ct)
    {
        var u = await auth.RegisterAsync(req, ct);
        return Created($"/users/{u.Id}", new { u.Id, u.Email, u.Role });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest req, CancellationToken ct)
    {
        var token = await auth.LoginAsync(req, ct);
        return Ok(new AuthResponse(token));
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me(CancellationToken ct)
    {
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
        if (sub is null) return Unauthorized();
        var u = await auth.GetMeAsync(Guid.Parse(sub), ct);
        return Ok(new { u.Id, u.Email, u.Role });
    }
}