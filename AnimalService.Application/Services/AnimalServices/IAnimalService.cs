using AnimalService.Domain.Contracts.Dtos;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;

namespace AnimalService.Application.Services.AnimalServices;

public interface IAnimalService
{
    Task<AnimalDto> Create(CreateAnimalRequest request);
    Task<List<AnimalDto>> GetAll(GetAllAnimalsRequest request);
    Task<AnimalDto> Update(UpdateAnimalRequest request);
    Task SoftDelete(int id);
}