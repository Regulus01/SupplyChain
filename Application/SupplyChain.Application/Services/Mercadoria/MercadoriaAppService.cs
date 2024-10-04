using AutoMapper;
using SupplyChain.Application.Interfaces;
using SupplyChain.Application.ValueObjects.Dto.Mercadoria;
using SupplyChain.Application.ValueObjects.ViewModels.Mercadoria;
using SupplyChain.Domain.Bus;
using SupplyChain.Domain.Interface.Repository;

namespace SupplyChain.Application.Services.Mercadoria;

public partial class MercadoriaAppService : IMercadoriaAppService
{
    private readonly IMercadoriaRepository _repository;
    private readonly Bus _bus;
    private readonly IMapper _mapper;

    public MercadoriaAppService(Bus bus, IMercadoriaRepository repository, IMapper mapper)
    {
        _bus = bus;
        _repository = repository;
        _mapper = mapper;
    }

    public CriarMercadoriaViewModel? CriarMercadoria(CriarMercadoriaDto dto)
    {
        var mercadoria = CriarMercadoriaDomain(dto);

        if (!Validar(mercadoria))
        {
            return null;
        } 
        
        if (!ValidarTipoEMercadoriaExistente(mercadoria.NumeroDeRegistro, mercadoria.TipoMercadoriaId, 
                                             out var tipoDeMercadoria))
        {
            return null;
        }
        
        _repository.Add(mercadoria);

        if (!SaveChanges())
        {
            return null;
        }
        
        return _mapper.Map<CriarMercadoriaViewModel>(mercadoria, options =>
        {
            options.Items["NomeDoTipoMercadoria"] = tipoDeMercadoria?.Nome;
        });
    }

    public CadastrarSaidaViewModel? CadastrarSaida(CadastrarSaidaDto dto)
    {
        throw new NotImplementedException();
    }
    


}