using AutoMapper;
using SupplyChain.Application.Interfaces;
using SupplyChain.Application.ValueObjects.Dto.Mercadoria;
using SupplyChain.Application.ValueObjects.ViewModels.Mercadoria;
using SupplyChain.Domain.Bus;
using SupplyChain.Domain.Entities;
using SupplyChain.Domain.Interface.Repository;
using MercadoriaDomain = SupplyChain.Domain.Entities.Mercadoria;

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

    public CadastrarEntradaViewModel? CadastrarEntrada(CadastrarEntradaDto dto)
    {
        var entrada = CriarEntrada(dto);

        if (!Validar(entrada))
        {
            return null;
        } 
        
        var mercadoria = _repository.Query<MercadoriaDomain>(x => x.Id.Equals(dto.MercadoriaId))
                                    .FirstOrDefault();

        if (mercadoria == null)
        {
            _bus.Notify.NewNotification("Erro", "A mercadoria informada não existe.");
            return null;
        }
        
        _repository.Add(entrada);
        
        if (!SaveChanges())
        {
            return null;
        }

        EstoqueCadastroATTRabbitmq(entrada);

        return _mapper.Map<CadastrarEntradaViewModel>(entrada, options =>
        {
            options.Items["NomeDaMercadoria"] = mercadoria.Nome;
        });
    }

    private void EstoqueCadastroATTRabbitmq(Entrada entrada)
    {
        //Logica do estoque mandar para fila depois
        var estoque = _repository.Query<Estoque>(x => x.Local.ToLower().Equals(entrada.Local.ToLower()))
                                 .FirstOrDefault();
                 
        if (estoque == null)
        {
            CriarEstoque(entrada);
        }
        
        else
        {
            AtualizarEstoque(estoque);
        }

        SaveChanges();
    }

    private void AtualizarEstoque(Estoque estoque)
    {
        estoque.AdicionarEstoque(estoque.Quantidade);
        _repository.Update(estoque);
    }

    private bool CriarEstoque(Entrada entrada)
    {
        var novoEstoque = new Estoque(entrada.Local, entrada.Quantidade, entrada.MercadoriaId);
            
        if (!Validar(entrada))
        {
            return true;
        } 
            
        _repository.Add(novoEstoque);
        return false;
    }
}