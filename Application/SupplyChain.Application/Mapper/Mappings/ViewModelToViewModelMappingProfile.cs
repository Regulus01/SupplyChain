using AutoMapper;
using SupplyChain.Application.ValueObjects.ViewModels.Mercadoria;
using SupplyChain.Application.ValueObjects.ViewModels.TipoDeMercadoria;
using SupplyChain.Domain.Database.ViewModels;

namespace SupplyChain.Application.Mapper.Mappings;

public class ViewModelToViewModelMappingProfile : Profile
{
    public ViewModelToViewModelMappingProfile()
    {
        CreateMap<DbObterTipoDeMercadoriaViewModel, ObterTipoDeMercadoriaViewModel>()
            .ForMember(x => x.Nome, opt => opt.MapFrom(x => x.Tip_Nome));
        
        CreateMap<DbObterMercadoriaViewModel, ObterMercadoriaViewModel>()
            .ForMember(x => x.Nome, opt => opt.MapFrom(x => x.Mer_Nome));
    }
}