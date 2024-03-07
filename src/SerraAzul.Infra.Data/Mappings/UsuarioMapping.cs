using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerraAzul.Domain.Entities;

namespace SerraAzul.Infra.Data.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder
            .Property(u => u.NomeCompleto)
            .HasMaxLength(120)
            .IsRequired();
        
        builder
            .Property(u => u.DataDeNascimento)
            .HasConversion(
                v => v.ToString("yyyy-MM-dd"),
                v => DateTime.Parse(v)
            )
            .IsRequired();

        
        builder
            .Property(u => u.Email)
            .HasMaxLength(120)
            .IsRequired();
        
        builder
            .Property(u => u.Senha)
            .HasMaxLength(250)
            .IsRequired();
        
        builder
            .Property(a => a.Cpf)
            .HasMaxLength(11)
            .IsRequired();
    }
}