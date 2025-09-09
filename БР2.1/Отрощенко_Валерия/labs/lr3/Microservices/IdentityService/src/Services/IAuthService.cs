using IdentityService.DTOs;
using IdentityService.Models;

namespace IdentityService.Services;

public interface IAuthService
{
    Task<User> RegisterAsync(RegisterRequest req, CancellationToken ct);
    Task<string> LoginAsync(LoginRequest req, CancellationToken ct);
    Task<User> GetMeAsync(Guid userId, CancellationToken ct);
}