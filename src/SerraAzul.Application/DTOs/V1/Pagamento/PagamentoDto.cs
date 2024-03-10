namespace SerraAzul.Application.DTOs.V1.Pagamento;

public class PagamentoDto
{
    public int Id { get; set; }
    public string NomeNoCartao { get; set; } = null!;
    public string NumeroDoCartao { get; set; } = null!;
    public DateTime DataDeVencimento { get; set; }
    public string Cvv { get; set; } = null!;
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
}