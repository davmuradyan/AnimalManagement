using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TranslationService.Domain.Entities;
using TranslationService.Infrastructure.Configurations;
using TranslationService.Infrastructure.Exceptions;

namespace TranslationService.Infrastructure.Database;

public class MainDbContext(DbContextOptions<MainDbContext> options) : DbContext(options)
{
    public DbSet<Translation> Translations { get; set; }
    public DbSet<Translation> GroupItems { get; set; }
    public DbSet<Translation> TranslationGroups { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            options => options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null));
    }

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