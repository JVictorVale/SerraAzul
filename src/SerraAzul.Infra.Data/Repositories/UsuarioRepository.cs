using Microsoft.EntityFrameworkCore;
using SerraAzul.Domain.Contracts.Repositories;
using SerraAzul.Domain.Entities;
using SerraAzul.Infra.Data.Context;

namespace SerraAzul.Infra.Data.Repositories;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(SerraAzulDbContext context) : base(context)
    {
    }

    public void Cadastrar(Usuario usuario)
    {
        Context.Usuarios.Add(usuario);
    }

    public void Atualizar(Usuario usuario)
    {
        Context.Usuarios.Update(usuario);
    }

    public async Task<Usuario?> ObterPorId(int id)
    {
        return await Context.Usuarios
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Usuario?> ObterPorEmail(string email)
    {
        return await Context.Usuarios
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(a => a.Email == email);
    }

    public async Task<List<Usuario>> ObterTodos()
    {
        return await Context.Usuarios
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }
}