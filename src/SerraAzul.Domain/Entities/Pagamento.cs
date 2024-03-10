using FluentValidation.Results;
using SerraAzul.Domain.Contracts.Interfaces;
using SerraAzul.Domain.Validators;

namespace SerraAzul.Domain.Entities;

public class Pagamento : Entity, IAggragateRoot
{
    public int UsuarioId { get; set; }
    public string NomeNoCartao { get; set; } = null!;
    public string NumeroDoCartao { get; set; } = null!;
    public DateTime DataDeVencimento { get; set; }
    public string Cvv { get; set; } = null!;
    
    public Usuario Usuario { get; set; } = null!;

    public override bool Validar(out ValidationResult validationResult)
    {
        validationResult = new PagamentoValidator().Validate(this);
        return validationResult.IsValid;
    }
}