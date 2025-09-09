using IdentityService.DTOs;
using IdentityService.Models;
using IdentityService.Repository;

namespace IdentityService.Services;

public class AuthService(IUserRepository users, IJwtTokenFactory jwtFactory, IConfiguration cfg) : IAuthService
{
    public async Task<User> RegisterAsync(RegisterRequest req, CancellationToken ct)
    {
        var email = req.Email.Trim().ToLowerInvariant();
        if (await users.GetByEmailAsync(email, ct) is not null)
            throw new InvalidOperationException("User exists");

        var u = new User
        {
            Id = Guid.NewGuid(),        
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
            Role = (req.Role is "employer" or "applicant") ? req.Role : "applicant",
            CreatedAt = DateTime.UtcNow
        };
        await users.AddAsync(u, ct);
        await users.SaveChangesAsync(ct);
        return u;
    }

    public async Task<string> LoginAsync(LoginRequest req, CancellationToken ct)
    {
        var email = req.Email.Trim().ToLowerInvariant();
        var u = await users.GetByEmailAsync(email, ct);
        if (u is null || !BCrypt.Net.BCrypt.Verify(req.Password, u.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        var issuer = cfg["Jwt:Issuer"]!;
        var audience = cfg["Jwt:Audience"]!;
        var key = cfg["Jwt:Key"]!;
        return jwtFactory.Issue(u.Id, u.Email, u.Role, issuer, audience, key);
    }

    public async Task<User> GetMeAsync(Guid userId, CancellationToken ct) =>
        await users.GetByIdAsync(userId, ct) ?? throw new KeyNotFoundException("User not found");
}