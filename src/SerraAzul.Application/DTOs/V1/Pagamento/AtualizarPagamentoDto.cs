namespace SerraAzul.Application.DTOs.V1.Pagamento;

public class AtualizarPagamentoDto
{
    public int Id { get; set; }
    public string NomeNoCartao { get; set; } = null!;
    public string NumeroDoCartao { get; set; } = null!;
    public DateTime DataDeVencimento { get; set; }
    public string Cvv { get; set; } = null!;
}