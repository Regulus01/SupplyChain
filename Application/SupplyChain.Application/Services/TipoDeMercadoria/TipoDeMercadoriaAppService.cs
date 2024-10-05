using AutoMapper;
using SupplyChain.Application.Interfaces;
using SupplyChain.Application.ValueObjects.Dto.TipoDeMercadoria;
using SupplyChain.Application.ValueObjects.ViewModels.TipoDeMercadoria;
using SupplyChain.Domain.Interface.Bus;
using SupplyChain.Domain.Interface.Repository;
using SupplyChain.Domain.Resourcers;
using TipoDeMercadoriaDomain = SupplyChain.Domain.Entities.TipoDeMercadoria;

namespace SupplyChain.Application.Services.TipoDeMercadoria;

public class TipoDeMercadoriaAppService : ITipoDeMercadoriaAppService
{
    private readonly ITipoDeMercadoriaRepository _repository;
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    public TipoDeMercadoriaAppService(ITipoDeMercadoriaRepository repository, IMapper mapper, IBus bus)
    {
        _repository = repository;
        _mapper = mapper;
        _bus = bus;
    }

    /// <inheritdoc />
    public CriarTipoDeMercadoriaViewModel? CriarTipoDeMercadoria(CriarTipoDeMercadoriaDto dto)
    {
        var tipoDeMercadoria = new TipoDeMercadoriaDomain(dto.Nome);

        if (ValidarTipoDeMercadoria(tipoDeMercadoria))
        {
            return null;
        }

        _repository.Add(tipoDeMercadoria);

        if (!_repository.SaveChanges())
        {
            _bus.Notify.NewNotification(ErrorMessage.ERRO_SALVAR.Code, 
                                        ErrorMessage.ERRO_SALVAR.Message);
            return null;
        }

        return _mapper.Map<CriarTipoDeMercadoriaViewModel>(tipoDeMercadoria);
    }

    /// <inheritdoc />
    public IEnumerable<ObterTipoDeMercadoriaViewModel> ObterTipoDeMercadoria(int? skip = null, int? take = null)
    {
        var tiposDeMercadorias = _repository.ObterListagem(skip, take);
        
        return _mapper.Map<IEnumerable<ObterTipoDeMercadoriaViewModel>>(tiposDeMercadorias);
    }

    /// <summary>
    /// Valida se tipoDeMercadoria é valido para a inserção no sistema
    /// </summary>
    /// <param name="tipoDeMercadoria">Entidade a ser validada</param>
    /// <returns>Retorna <c>false</c> se o tipoDeMercadoria está inválida e <c>true</c> caso válida.</returns>
    private bool ValidarTipoDeMercadoria(TipoDeMercadoriaDomain tipoDeMercadoria)
    {
        var validacao = tipoDeMercadoria.Validate();

        if (!validacao.IsValid)
        {
            _bus.Notify.NewNotification(validacao.Erros);
            return false;
        }

        return false;
    }
}