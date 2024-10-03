using AutoMapper;
using SupplyChain.Application.Interfaces;
using SupplyChain.Application.ValueObjects.Dto.Mercadoria;
using SupplyChain.Application.ValueObjects.ViewModels.Mercadoria;
using SupplyChain.Domain.Bus;
using SupplyChain.Domain.Interface.Repository;
using SupplyChain.Domain.Resourcers;
using TipoDeMercadoriaDomain = SupplyChain.Domain.Entities.TipoDeMercadoria;
using MercadoriaDomain = SupplyChain.Domain.Entities.Mercadoria;

namespace SupplyChain.Application.Services.Mercadoria;

public class MercadoriaAppService : IMercadoriaAppService
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

        if (!_repository.SaveChanges())
        {
            _bus.Notify.NewNotification(ErrorMessage.ERRO_SALVAR.Code, 
                                        ErrorMessage.ERRO_SALVAR.Message);
            return null;
        }

        return _mapper.Map<CriarMercadoriaViewModel>(mercadoria, options =>
        {
            options.Items["NomeDoTipoMercadoria"] = tipoDeMercadoria?.Nome;
        });
    }

    /// <summary>
    /// Valida se uma mercadoria já existe no sistema com o mesmo código e se o tipo de mercadoria fornecido é válido.
    /// </summary>
    /// <param name="codigoDaMercadoria">Código da mercadoria a ser verificado.</param>
    /// <param name="tipoDaMercadoriaId">Identificador do tipo de mercadoria a ser validado.</param>
    /// <param name="tipoDeMercadoria">Retorna o objeto TipoDeMercadoriaDomain correspondente se encontrado,
    /// caso contrário, será null.</param>
    /// <returns>Retorna <c>false</c> se a mercadoria está inválida e <c>true</c> caso válida.</returns>
    private bool ValidarTipoEMercadoriaExistente(string codigoDaMercadoria, Guid tipoDaMercadoriaId,
        out TipoDeMercadoriaDomain? tipoDeMercadoria)
    {
        tipoDeMercadoria = _repository.Query<TipoDeMercadoriaDomain>(x => x.Id.Equals(tipoDaMercadoriaId))
                                      .FirstOrDefault();
        
        var mercadoria = _repository.Query<MercadoriaDomain>(x => x.NumeroDeRegistro.Equals(codigoDaMercadoria))
            .FirstOrDefault();

        if (mercadoria != null)
        {
            _bus.Notify.NewNotification(ErrorMessage.MER_CODIGOS_IGUAIS.Code, 
                ErrorMessage.MER_CODIGOS_IGUAIS.Message);
            return false;
        }
        
        if (tipoDeMercadoria == null)
        {
            _bus.Notify.NewNotification(ErrorMessage.TIP_MERCADORIA_NAO_EXISTE.Code, 
                ErrorMessage.TIP_MERCADORIA_NAO_EXISTE.Message);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Valida se uma mercadoria é valida para a inserção no sistema
    /// </summary>
    /// <param name="mercadoriaDomain">Entidade de mercadoria </param>
    /// <returns>Retorna <c>false</c> se a mercadoria está inválida e <c>true</c> caso válida.</returns>
    private bool Validar(MercadoriaDomain mercadoriaDomain)
    {
        var validacao = mercadoriaDomain.Validate();
        
        if (!validacao.IsValid)
        {
            _bus.Notify.NewNotification(validacao.Erros);
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Cria uma entidade de dominio de mercadoria a partir de um dto
    /// </summary>
    /// <param name="dto">Dados necessários para a criação</param>
    /// <returns>Entidade de dominio de mercadoria</returns>
    private MercadoriaDomain CriarMercadoriaDomain(CriarMercadoriaDto dto)
    {
        return new MercadoriaDomain(
            dto.NumeroDeRegistro,
            dto.Nome,
            dto.Fabricante,
            dto.Descricao,
            dto.TipoMercadoriaId
        );
    }
}