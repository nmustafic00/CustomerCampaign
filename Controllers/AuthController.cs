using CustomerCampaign.DTOs;
using CustomerCampaign.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);
        if (token == null)
            return Unauthorized();

        return Ok(new { token });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create-agent")]
    public async Task<IActionResult> CreateAgent([FromBody] CreateAgentDto dto)
    {
        var user = await _authService.CreateAgentAsync(dto);
        return Ok(user);
    }
}
