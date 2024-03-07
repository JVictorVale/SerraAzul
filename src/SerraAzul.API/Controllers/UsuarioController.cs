using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SerraAzul.Application.Contracts;
using SerraAzul.Application.DTOs.V1.User;
using SerraAzul.Application.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace SerraAzul.API.Controllers;

[Route("usuario/[controller]")]
public class UsuarioController : BaseController
{
    private readonly IUsuarioService _usuarioService;
    
    public UsuarioController(INotificator notificator, IUsuarioService usuarioService) : base(notificator)
    {
        _usuarioService = usuarioService;
    }
    
    [AllowAnonymous]
    [HttpPost]
    [SwaggerOperation(Summary = "Cadastro de um Usuário", Tags = new[] { "Usuário - Usuarios" })]
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cadastrar([FromForm] AdicionarUsuarioDto dto)
    {
        return OkResponse(await _usuarioService.Cadastrar(dto));
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualizar um Usuário", Tags = new[] { "Usuário - Usuarios" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Atualizar(int id, [FromForm] AtualizarUsuarioDto dto)
    {
        return OkResponse(await _usuarioService.Atualizar(id, dto));
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obter um Usuário", Tags = new[] { "Usuário - Usuarios" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        return OkResponse(await _usuarioService.ObterPorId(id));
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Obter todos", Tags = new[] { "Usuário - Usuarios" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ObterTodos()
    {
        return OkResponse(await _usuarioService.ObterTodos());
    }
}