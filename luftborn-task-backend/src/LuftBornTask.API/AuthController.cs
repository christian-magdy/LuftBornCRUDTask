using Microsoft.AspNetCore.Mvc;
using LuftBornTask.Application.DTOs;
using LuftBornTask.Infrastructure.Persistence;
using LuftBornTask.Infrastructure.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext context;
    private readonly JwtService jwtService;

    public AuthController(AppDbContext context, JwtService jwtService)
    {
        this.context = context;
        this.jwtService = jwtService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var user = context.Users
            .FirstOrDefault(x => x.Username == dto.Username);

        if (user == null)
            return Unauthorized("Invalid username");

        if (user.PasswordHash != Hash(dto.Password))
            return Unauthorized("Invalid password");

        var token = jwtService.GenerateToken(user.Username);

        return Ok(new
        {
            token
        });
    }

    private string Hash(string password)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }
}