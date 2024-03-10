namespace SerraAzul.Application.DTOs.V1.Usuario;

public class AtualizarUsuarioDto
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = null!;
    public DateTime DataDeNascimento { get; set; }
    public string Cpf { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public string ConfirmarSenha { get; set; } = null!;
}