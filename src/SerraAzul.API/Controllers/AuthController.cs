using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SerraAzul.Application.Contracts;
using SerraAzul.Application.DTOs.V1.Auth;
using SerraAzul.Application.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace SerraAzul.API.Controllers;

[AllowAnonymous]
[Route("auth/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;
    
    public AuthController(INotificator notificator, IAuthService authService) : base(notificator)
    {
        _authService = authService;
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "Login", Tags = new[] { "Usuário - Auth" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody]LoginDto dto)
    {
        var usuario = await _authService.Login(dto);
        return usuario != null ? OkResponse(usuario) : Unauthorized(new[] { "Usuário e/ou senha incorretos" });
    }
}