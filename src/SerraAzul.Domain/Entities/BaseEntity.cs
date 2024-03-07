using SerraAzul.Domain.Contracts.Interfaces;

namespace SerraAzul.Domain.Entities;

public abstract class BaseEntity : IEntity
{
    public int Id { get; set; }
}