using AnimalManagement.Shared.Dtos;
using AnimalService.Domain.Contracts;
using AnimalService.Domain.Contracts.Dtos;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;

namespace AnimalService.Application.Services.AnimalServices;

public interface IAnimalService
{
    Task<ErrorOr<AnimalDto>> Create(CreateAnimalRequest request);
    Task<ErrorOr<List<AnimalDto>>> GetAll(GetAllAnimalsRequest request);
    Task<ErrorOr<AnimalDto>> Update(UpdateAnimalRequest request);
    Task<ErrorOr<bool>> SoftDelete(int id);
}