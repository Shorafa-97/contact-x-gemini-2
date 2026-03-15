using AutoMapper;
using ContactsX.Application.DTOs.Entity;
using ContactsX.Domain.Entities;


namespace ContactsX.Infrastructure.Mappings;

public class EntityProfile : Profile
{
    public EntityProfile()
    {
        CreateMap<CreateEntityDto, Entity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<UpdateEntityDto, Entity>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}