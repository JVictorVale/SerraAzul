using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerraAzul.Domain.Entities;

namespace SerraAzul.Infra.Data.Mappings;

public class PagamentoMapping : IEntityTypeConfiguration<Pagamento>
{
    public void Configure(EntityTypeBuilder<Pagamento> builder)
    {
        builder
            .Property(p => p.NomeNoCartao)
            .HasMaxLength(120)
            .IsRequired();

        builder
            .Property(p => p.NumeroDoCartao)
            .HasMaxLength(16)
            .IsRequired();
        
        builder
            .Property(p => p.DataDeVencimento)
            .IsRequired()
            .HasConversion(
                v => v.ToString("MM/yy"), 
                v => DateTime.ParseExact(v, "MM/yy", null)
            );

        builder
            .Property(p => p.Cvv)
            .HasMaxLength(3)
            .IsRequired();
    }
}