using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SerraAzul.Domain.Contracts.Interfaces;
using SerraAzul.Domain.Entities;
using SerraAzul.Infra.Data.Extensions;

namespace SerraAzul.Infra.Data.Context;

public class SerraAzulDbContext : DbContext, IUnitOfWork
{
    public SerraAzulDbContext(DbContextOptions<SerraAzulDbContext> options) : base(options)
    {
        
    }

    public DbSet<Usuario> Usuarios { get; set; } = null!;
    
    public async Task<bool> Commit() => await SaveChangesAsync() > 0;
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        ApplyTrackingChanges();
        return base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ApplyConfigurations(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
    
    private void ApplyTrackingChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is ITracking && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            ((ITracking)entityEntry.Entity).AtualizadoEm = DateTime.Now;
            
            if (entityEntry.State != EntityState.Added)
                continue;

            ((ITracking)entityEntry.Entity).CriadoEm = DateTime.Now;
        }
    }
    
    private static void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        
        modelBuilder.ApplyEntityConfiguration();
        modelBuilder.ApplyTrackingConfiguration();
    }
}