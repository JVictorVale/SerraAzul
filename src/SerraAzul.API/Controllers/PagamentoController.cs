using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SerraAzul.Application.Contracts;
using SerraAzul.Application.DTOs.V1.Pagamento;
using SerraAzul.Application.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace SerraAzul.API.Controllers;

[Authorize]
[Route("pagamento/[controller]")]
public class PagamentoController : BaseController
{
    private readonly IPagamentoService _pagamentoService;
    
    public PagamentoController(INotificator notificator, IPagamentoService pagamentoService) : base(notificator)
    {
        _pagamentoService = pagamentoService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Adicionar de um Pagamento", Tags = new[] { "Pagamento - Pagamentos" })]
    [ProducesResponseType(typeof(PagamentoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Adicionar([FromForm] AdicionarPagamentoDto dto)
    {
        return OkResponse(await _pagamentoService.Adicionar(dto));
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualizar um Pagamento", Tags = new[] { "Pagamento - Pagamentos" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Atualizar(int id, [FromForm] AtualizarPagamentoDto dto)
    {
        return OkResponse(await _pagamentoService.Atualizar(id, dto));
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Obter um Pagamento", Tags = new[] { "Pagamento - Pagamentos" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        return OkResponse(await _pagamentoService.ObterPorId(id));
    }
}