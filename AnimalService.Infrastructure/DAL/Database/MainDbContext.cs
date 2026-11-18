using AnimalService.Domain.Entities;
using AnimalService.Infrastructure.DAL.Configurations;
using AnimalService.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AnimalService.Infrastructure.DAL.Database;

public class MainDbContext(DbContextOptions<MainDbContext> options) : DbContext(options)
{
    public DbSet<Animal> Animals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddConfigurations();
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new CustomConcurrencyException(ex);
        }
        catch (RetryLimitExceededException ex)
        {
            throw new CustomRetryLimitExceededException(ex);
        }
        catch (DbUpdateException ex)
        {
            throw new CustomDbUpdateException(ex);
        }
    }
}