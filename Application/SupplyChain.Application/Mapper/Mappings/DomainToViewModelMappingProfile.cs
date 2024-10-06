using AutoMapper;
using SupplyChain.Application.ValueObjects.ViewModels.Estoque;
using SupplyChain.Application.ValueObjects.ViewModels.Mercadoria;
using SupplyChain.Application.ValueObjects.ViewModels.TipoDeMercadoria;
using SupplyChain.Domain.Entities;

namespace SupplyChain.Application.Mapper.Mappings;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        MercadoriaMap();
        EstoqueMap();
        TipoDeMercadoriaMap();
    }

    /// <summary>
    /// Mapeamentos destinados ao fluxo de mercadoria
    /// </summary>
    private void MercadoriaMap()
    {
        CreateMap<Mercadoria, CriarMercadoriaViewModel>()
            .ForMember(x => x.TipoDeMercadoria,
                opt => { opt.MapFrom((_, _, _, ctx) => ctx.Items["NomeDoTipoMercadoria"]); });
    }

    /// <summary>
    /// Mapeamento destinado aos fluxos de tipo de mercadoria
    /// </summary>
    private void TipoDeMercadoriaMap()
    {
        CreateMap<TipoDeMercadoria, CriarTipoDeMercadoriaViewModel>();
    }

    /// <summary>
    /// Mapeamentos destinados aos fluxos de estoque
    /// </summary>
    private void EstoqueMap()
    {
        CreateMap<Estoque, CadastrarEstoqueViewModel>();
        CreateMap<Entrada, ObterEntradasMensaisViewModel>()
            .ForMember(x => x.Mercadoria, opt => opt.MapFrom(x => x.Mercadoria.Nome))
            .ForMember(x => x.DataDaEntrada, opt => opt.MapFrom(x => x.DataDaEntrada.Date));

        CreateMap<Saida, CadastrarSaidaViewModel>();
        CreateMap<Saida, ObterSaidasMensaisViewModel>()
            .ForMember(x => x.Mercadoria, opt => opt.MapFrom(x => x.Mercadoria.Nome))
            .ForMember(x => x.DataDaSaida, opt => opt.MapFrom(x => x.DataDaSaida.Date));
        
        CreateMap<Entrada, CadastrarEntradaViewModel>()
            .ForMember(x => x.NomeDaMercadoria,
                opt => { opt.MapFrom((_, _, _, ctx) => ctx.Items["NomeDaMercadoria"]); });
    }
}