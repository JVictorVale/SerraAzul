using FluentValidation.Results;
using SerraAzul.Domain.Contracts.Interfaces;
using SerraAzul.Domain.Validators;

namespace SerraAzul.Domain.Entities;

public class Usuario : Entity, IAggragateRoot, ISoftDelete
{
    public string NomeCompleto { get; set; } = null!;
    public DateTime DataDeNascimento { get; set; }
    public string Cpf { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;

    public Pagamento Pagamento { get; set; } = null!;

    public override bool Validar(out ValidationResult validationResult)
    {
        validationResult = new UsuarioValidator().Validate(this);
        return validationResult.IsValid;
    }
}