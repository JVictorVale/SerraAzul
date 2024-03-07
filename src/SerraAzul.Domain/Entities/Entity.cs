using FluentValidation.Results;
using SerraAzul.Domain.Contracts.Interfaces;

namespace SerraAzul.Domain.Entities;

public abstract class Entity : BaseEntity, ITracking
{
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
    
    public virtual bool Validar(out ValidationResult validationResult)
    {
        validationResult = new ValidationResult();
        return validationResult.IsValid;
    }
}