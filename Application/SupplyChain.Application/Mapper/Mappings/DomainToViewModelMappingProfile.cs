using AutoMapper;
using SupplyChain.Application.ValueObjects.ViewModels;
using SupplyChain.Domain.Entities;

namespace SupplyChain.Application.Mapper.Mappings;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        CreateMap<Mercadoria, CriarMercadoriaViewModel>()
            .ForMember(x => x.TipoDeMercadoria,
                opt =>
                {
                    opt.MapFrom((_, _, _, ctx) => ctx.Items["NomeDoTipoMercadoria"]); 
                });
    }
}