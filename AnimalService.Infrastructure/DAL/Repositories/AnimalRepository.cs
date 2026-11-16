using AnimalService.Application.Interfaces.Repositories;
using AnimalService.Domain.Entities;
using AnimalService.Domain.HelperClasses;
using AnimalService.Infrastructure.DAL.Database;
using Microsoft.EntityFrameworkCore;

namespace AnimalService.Infrastructure.DAL.Repositories;

public class AnimalRepository(MainDbContext context) : IAnimalRepository
{
    public async Task Create(Animal animal)
    {
        await context.Animals.AddAsync(animal);
        await context.SaveChangesAsync();
    }

    public async Task<List<Animal>> GetAll()
    {
        var result = context.Animals.AsQueryable();
        return await result.ToListAsync();
    }

    public async Task<List<Animal>> GetAll(
        int pageNumber, 
        int pageSize, 
        FilterArgs args)
    {
        var query = context.Animals
            .AsNoTracking()
            .AsQueryable();

        if (!args.IncludeClosed)
            query = query.Where(a => a.ClosedDate == null);

        if (args.Colors is { Length: > 0 })
            query = query.Where(a => args.Colors.Contains(a.Color));
        
        if (args.Gender != null)
            query = query.Where(a => args.Gender == a.Gender);
        
        if (args.MinAge != null)
            query = query.Where(a => a.Age >= args.MinAge);
        
        if (args.MaxAge != null)
            query = query.Where(a => a.Age <= args.MaxAge);
        
        if (args.MinWeight != null)
            query = query.Where(a => a.Weight >= args.MinWeight);
        
        if (args.MaxWeight != null)
            query = query.Where(a => a.Weight <= args.MaxWeight);
        
        query = query.OrderBy(a => a.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(args.Count ?? pageSize);
        
        return await query.ToListAsync();
    }

    public async Task<Animal?> GetAnimal(int animalId)
    {
        var result = await context.Animals.FirstOrDefaultAsync(animal => animal.Id == animalId);
        return result;
    }

    public async Task Update(Animal animal)
    {
        await context.SaveChangesAsync();
    }

    public async Task Delete(Animal animal)
    {
        context.Animals.Remove(animal);
        await context.SaveChangesAsync();
    }
}