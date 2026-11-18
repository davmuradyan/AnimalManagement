using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UserService.Domain.Entities;
using UserService.Infrastructure.DAL.Configurations;
using UserService.Infrastructure.Exceptions;

namespace UserService.Infrastructure.DAL.Database;

public class MainDbContext(DbContextOptions<MainDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
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
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
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