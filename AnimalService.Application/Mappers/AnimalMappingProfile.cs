using AnimalManagement.Shared.Enums;
using AnimalService.Domain.Contracts.Dtos;
using AnimalService.Domain.Entities;
using AutoMapper;

namespace AnimalService.Application.Mappers;

public class AnimalMappingProfile : Profile
{
    public AnimalMappingProfile()
    {
        // Entity -> DTO
        CreateMap<Animal, AnimalDto>()
            .ForMember(dest => dest.Gender,
                opt => opt.MapFrom(src => src.Gender.ToString()))
            .ForMember(dest => dest.Color,
                opt => opt.MapFrom(src => src.Color.ToString()));

        // DTO -> Entity
        CreateMap<AnimalDto, Animal>().ConstructUsing(src => Animal.Create(
                src.Name,
                src.Age,
                src.Weight,
                Enum.Parse<Gender>(src.Gender),
                Enum.Parse<Color>(src.Color)
            ));
    }
}