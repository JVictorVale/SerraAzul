using SerraAzul.Application.DTOs.V1.User;

namespace SerraAzul.Application.Contracts;

public interface IUsuarioService
{
    Task<UsuarioDto?> Cadastrar(AdicionarUsuarioDto usuarioDto);
    Task<UsuarioDto?> Atualizar(int id, AtualizarUsuarioDto usuarioDto);
    Task<UsuarioDto> ObterPorId(int id);
    Task<UsuarioDto> ObterPorEmail(string email);
    Task<List<UsuarioDto>> ObterTodos();
}