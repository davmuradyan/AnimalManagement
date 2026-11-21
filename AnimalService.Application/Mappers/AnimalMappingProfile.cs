using AnimalService.Domain.Contracts.Dtos;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;
using AnimalService.Domain.Entities;
using AutoMapper;

namespace AnimalService.Application.Mappers;

public class AnimalMappingProfile : Profile
{
    public AnimalMappingProfile()
    {
        // Entity -> DTO
        CreateMap<Animal, AnimalDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Age, o => o.MapFrom(s => s.Age))
            .ForMember(d => d.Weight, o => o.MapFrom(s => s.Weight))
            .ForMember(d => d.Gender, o => o.MapFrom(s => s.Gender.ToString()))
            .ForMember(d => d.Color, o => o.MapFrom(s => s.Color.ToString()))
            .ForMember(d => d.RowVersion, o => o.MapFrom(s => s.RowVersion));

        CreateMap<CreateAnimalRequest, Animal>()
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Age, o => o.MapFrom(s => s.Age))
            .ForMember(d => d.Weight, o => o.MapFrom(s => s.Weight));

    }
}