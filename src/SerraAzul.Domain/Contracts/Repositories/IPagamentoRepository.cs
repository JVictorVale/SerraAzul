using SerraAzul.Domain.Entities;

namespace SerraAzul.Domain.Contracts.Repositories;

public interface IPagamentoRepository : IRepository<Pagamento>
{
    void Adicionar(Pagamento pagamento);
    void Atualizar(Pagamento pagamento);
    Task<Pagamento?> ObterPorId(int id, int? usuarioid);
}