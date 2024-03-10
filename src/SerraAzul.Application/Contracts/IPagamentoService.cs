using SerraAzul.Application.DTOs.V1.Pagamento;

namespace SerraAzul.Application.Contracts;

public interface IPagamentoService
{
    Task<PagamentoDto?> Adicionar(AdicionarPagamentoDto pagamentoDto);
    Task<PagamentoDto?> Atualizar(int id, AtualizarPagamentoDto pagamentoDto);
    Task<PagamentoDto?> ObterPorId(int id);
}