using Microsoft.EntityFrameworkCore;
using SerraAzul.Domain.Contracts.Repositories;
using SerraAzul.Domain.Entities;
using SerraAzul.Infra.Data.Context;

namespace SerraAzul.Infra.Data.Repositories;

public class PagamentoRepository : Repository<Pagamento>, IPagamentoRepository
{
    public PagamentoRepository(SerraAzulDbContext context) : base(context)
    {
    }

    public void Adicionar(Pagamento pagamento)
    {
        Context.Pagamentos.Add(pagamento);
    }

    public void Atualizar(Pagamento pagamento)
    {
        Context.Pagamentos.Update(pagamento);
    }

    public async Task<Pagamento?> ObterPorId(int id, int? usuarioid)
    {
        return await Context.Pagamentos.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == usuarioid);
    }
}