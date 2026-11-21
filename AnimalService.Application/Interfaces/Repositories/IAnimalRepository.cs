using AnimalService.Domain.Entities;
using AnimalService.Domain.HelperClasses;

namespace AnimalService.Application.Interfaces.Repositories;

public interface IAnimalRepository
{
    Task Create(Animal animal);
    Task<Animal?> GetAnimal(int animalId);
    Task<List<Animal>> GetAll(int pageNumber, int pageSize, FilterArgs args);
    Task Update(Animal animal);
    void SetOriginalRowVersion(Animal animal, byte[] rowVersion);
    Task Delete(Animal animal);
}