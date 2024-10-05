using AutoMapper;
using SupplyChain.Application.Mapper.Mappings;

namespace SupplyChain.Application.Mapper;

public class MappingConfiguration
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(config =>
        {
            config.AddProfile(new DomainToViewModelMappingProfile());
            config.AddProfile(new ViewModelToViewModelMappingProfile());
        });
    }
}