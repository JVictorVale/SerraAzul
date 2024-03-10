using AutoMapper;
using Microsoft.AspNetCore.Http;
using SerraAzul.Application.Contracts;
using SerraAzul.Application.DTOs.V1.Pagamento;
using SerraAzul.Application.Notifications;
using SerraAzul.Core.Extensions;
using SerraAzul.Domain.Contracts.Repositories;
using SerraAzul.Domain.Entities;

namespace SerraAzul.Application.Services;

public class PagamentoService : BaseService, IPagamentoService
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public PagamentoService(IMapper mapper, INotificator notificator, IPagamentoRepository pagamentoRepository, IHttpContextAccessor httpContextAccessor) : base(mapper, notificator)
    {
        _pagamentoRepository = pagamentoRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PagamentoDto?> Adicionar(AdicionarPagamentoDto pagamentoDto)
    {
        var pagamento = Mapper.Map<Pagamento>(pagamentoDto);
        pagamento.UsuarioId = _httpContextAccessor.GetUserId() ?? 0;

        if (!await Validar(pagamento)) return null;

        _pagamentoRepository.Adicionar(pagamento);

        if (await _pagamentoRepository.UnitOfWork.Commit())
            return Mapper.Map<PagamentoDto>(pagamento);

        Notificator.Handle("Não foi possível finalizar o pagamento");
        return null;
    }

    public async Task<PagamentoDto?> Atualizar(int id, AtualizarPagamentoDto pagamentoDto)
    {
        if (id != pagamentoDto.Id)
        {
            Notificator.Handle("Os ids não conferem!");
        }

        var pagamento = await _pagamentoRepository.ObterPorId(id, _httpContextAccessor.GetUserId());
        if (pagamento == null)
        {
            Notificator.HandleNotFoundResource();
            return null;
        }

        Mapper.Map(pagamentoDto, pagamento);
        if (!await Validar(pagamento))
        {
            return null;
        }
        
        _pagamentoRepository.Atualizar(pagamento);
        if (await _pagamentoRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<PagamentoDto>(pagamento);
        }
        
        Notificator.Handle("Não foi possível atualizar os dados do pagamento.");
        return null;
    }

    public async Task<PagamentoDto?> ObterPorId(int id)
    {
        var pagamento = await _pagamentoRepository.ObterPorId(id, _httpContextAccessor.GetUserId());
        if (pagamento != null)
        {
            return Mapper.Map<PagamentoDto>(pagamento);
        }
        
        Notificator.HandleNotFoundResource();
        return null;
    }
    
    private async Task<bool> Validar(Pagamento pagamento)
    {
        if (!pagamento.Validar(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
            return false;
        }

        // Verifica se já existe um pagamento para este usuário
        var pagamentoExistente = await _pagamentoRepository.FirstOrDefault(c => c.UsuarioId == pagamento.UsuarioId);

        if (pagamentoExistente != null && pagamentoExistente.Id != pagamento.Id)
        {
            Notificator.Handle("Já existe pagamento para este usuário");
            return false;
        }

        return !Notificator.HasNotification;
    }


}