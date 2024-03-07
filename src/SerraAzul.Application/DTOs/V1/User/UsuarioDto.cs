namespace SerraAzul.Application.DTOs.V1.User;

public class UsuarioDto
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = null!;
    public DateTime DataDeNascimento { get; set; }
    public string Cpf { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
}