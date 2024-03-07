namespace SerraAzul.Application.DTOs.V1.User;

public class AdicionarUsuarioDto
{
    public string NomeCompleto { get; set; } = null!;
    public DateTime DataDeNascimento { get; set; }
    public string Cpf { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public string ConfirmarSenha { get; set; } = null!;
}