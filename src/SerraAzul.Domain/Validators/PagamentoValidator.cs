using FluentValidation;
using SerraAzul.Domain.Entities;

namespace SerraAzul.Domain.Validators;

public class PagamentoValidator : AbstractValidator<Pagamento>
{
    public PagamentoValidator()
    {
        RuleFor(p => p.NomeNoCartao)
            .NotEmpty()
            .WithMessage("O nome no cartão deve ser informado.")
            .MaximumLength(120)
            .WithMessage("O nome no cartão deve conter no máximo 120 caracteres.");

        RuleFor(p => p.NumeroDoCartao)
            .NotEmpty()
            .WithMessage("Número do cartão deve ser informado.")
            .Length(13,16)
            .WithMessage("Número do cartão deve conter entre 13 e 16 caracteres.");

        RuleFor(p => p.DataDeVencimento)
            .NotEmpty()
            .WithMessage("A data de vencimento deve ser informada.")
            .GreaterThan(DateTime.Now)
            .WithMessage("A data de vencimento deve ser maior que a data atual.");

        RuleFor(p => p.Cvv)
            .NotEmpty()
            .WithMessage("O código CVV deve ser informado")
            .Length(3)
            .WithMessage("O código CVV deve conter exatamente 3 caracteres.");
        
    }

}