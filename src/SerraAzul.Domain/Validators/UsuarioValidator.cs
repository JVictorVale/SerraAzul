using FluentValidation;
using SerraAzul.Domain.Entities;

namespace SerraAzul.Domain.Validators;

public class UsuarioValidator : AbstractValidator<Usuario>
{
    public UsuarioValidator()
    {
        RuleFor(u => u.NomeCompleto)
            .NotEmpty()
            .WithMessage("O nome não pode ser vazio")
            .Length(10, 120)
            .WithMessage("O nome deve ter no mínimo {MinLength} e no máximo {MaxLength} caracteres");

        RuleFor(u => u.Email)
            .EmailAddress();
        
        RuleFor(u => u.Senha)
            .NotEmpty()
            .WithMessage("A senha não pode ser vazia")
            .Length(6,8)
            .WithMessage("A senha deve ter no mínimo {MinLength} e máximo {MaxLength} caracteres");
        
        RuleFor(u => u.Cpf)
            .NotEmpty()
            .WithMessage("O Cpf não pode ser vazio")
            .Length(11, 14)
            .WithMessage("O Cpf deve ter no mínimo {MinLength} e no máximo {MaxLength} caracteres");
        
        RuleFor(u => u.DataDeNascimento)
            .NotEmpty()
            .WithMessage("A data de nascimento não pode ser vazia")
            .Must(date => date <= DateTime.Now)
            .WithMessage("A data de nascimento não pode ser no futuro");

    }
}