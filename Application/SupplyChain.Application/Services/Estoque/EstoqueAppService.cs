using System.Xml.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Application.Interfaces;
using SupplyChain.Application.ValueObjects.Dto.Estoque;
using SupplyChain.Application.ValueObjects.ViewModels.Estoque;
using SupplyChain.Domain.Entities;
using SupplyChain.Domain.Interface.Bus;
using SupplyChain.Domain.Interface.Repository;
using EstoqueDomain = SupplyChain.Domain.Entities.Estoque;

namespace SupplyChain.Application.Services.Estoque;

/// <inheritdoc />
public partial class EstoqueAppService : IEstoqueAppService
{
    private readonly IEstoqueRepository _repository;
    private readonly IBus _bus;
    private readonly IMapper _mapper;

    public EstoqueAppService(IEstoqueRepository repository, IMapper mapper, IBus bus)
    {
        _repository = repository;
        _mapper = mapper;
        _bus = bus;
    }

    /// <inheritdoc />
    public CadastrarEstoqueViewModel? CadastrarEstoque(CadastrarEstoqueDto dto)
    {
        var estoqueExistente = _repository.Query<EstoqueDomain>(x => x.Local.ToLower().Equals(dto.Local.ToLower()) &&
                                                                     x.MercadoriaId == dto.MercadoriaId)
            .FirstOrDefault();

        if (estoqueExistente != null)
        {
            _bus.Notify.NewNotification("Erro", "Já existe um estoque para o local informado.");
            return null;
        }

        var estoque = CriarEstoque(dto);

        if (!Validar(estoque))
            return null;

        _repository.Add(estoque);

        if (!SaveChanges())
            return null;

        return _mapper.Map<CadastrarEstoqueViewModel>(estoque);
    }

    /// <inheritdoc />
    public CadastrarEntradaViewModel? CadastrarEntrada(CadastrarEntradaDto dto)
    {
        if (!ValidarMercadoriaExistente(dto.MercadoriaId, out var mercadoria))
            return null;

        if (!ValidarLocalDoEstoqueExistente(dto.Local, dto.MercadoriaId, out var estoque))
            return null;

        var entrada = CriarEntrada(dto);

        if (!Validar(entrada))
            return null;

        _repository.Add(entrada);

        estoque?.AdicionarEstoque(entrada.Quantidade);

        if (estoque != null)
            _repository.Update(estoque);

        if (!SaveChanges())
            return null;

        return _mapper.Map<CadastrarEntradaViewModel>(entrada,
            options => { options.Items["NomeDaMercadoria"] = mercadoria?.Nome; });
    }

    /// <inheritdoc />
    public CadastrarSaidaViewModel? CadastrarSaida(CadastrarSaidaDto dto)
    {
        if (!ValidarLocalDoEstoqueExistente(dto.Local, dto.MercadoriaId, out var estoque))
            return null;

        if (!ValidarEstoque(dto, estoque))
            return null;

        var saida = CriarSaida(dto);

        if (!Validar(saida))
            return null;

        _repository.Add(saida);

        estoque?.RemoverEstoque(saida.Quantidade);

        if (estoque != null)
            _repository.Update(estoque);

        if (!SaveChanges())
            return null;

        return _mapper.Map<CadastrarSaidaViewModel>(saida);
    }

    /// <inheritdoc />
    public IEnumerable<ObterLocaisDeEstoqueViewModel> ObterLocaisDoEstoqueDaMercadoria(Guid mercadoriaId,
        int? skip = null, int? take = null)
    {
        var locais = _repository.ObterListagemDeLocais(mercadoriaId, skip, take);

        return _mapper.Map<IEnumerable<ObterLocaisDeEstoqueViewModel>>(locais);
    }

    /// <inheritdoc />
    public Dictionary<string, TotaisMensaisViewModel>? ObterRelatorioAnual(Guid mercadoriaId, int ano)
    {
        if (ano < 1900 || ano > DateTime.Now.Year)
        {
            _bus.Notify.NewNotification("Erro", "O ano precisa estar entre 1900 e o ano atual");
            return new Dictionary<string, TotaisMensaisViewModel>();
        }

        var totaisMensais = GerarDicionarioDeTotaisMensais(out var cultura);

        AtualizarTotaisEntradasPorMes(cultura, totaisMensais, mercadoriaId, ano);

        AtualizarTotaisSaidasPorMes(cultura, totaisMensais, mercadoriaId, ano);

        return totaisMensais;
    }

    /// <inheritdoc />
    public IEnumerable<ObterEntradasMensaisViewModel> ObterEntradasMensais(Guid mercadoriaId, int ano, int mes)
    {
        ValidarMesEAno(ano, mes);

        if (_bus.Notify.HasNotifications())
            return new List<ObterEntradasMensaisViewModel>();

        var entradas = _repository.Query<Entrada>(x => x.MercadoriaId.Equals(mercadoriaId) &&
                                                       x.DataDaEntrada.Year == ano &&
                                                       x.DataDaEntrada.Month == mes,
                                                       includes: y => y.Include(i => i.Mercadoria)).ToList();

        return _mapper.Map<IEnumerable<ObterEntradasMensaisViewModel>>(entradas);
    }
    
    /// <inheritdoc />
    public IEnumerable<ObterSaidasMensaisViewModel> ObterSaidasMensais(Guid mercadoriaId, int ano, int mes)
    {
        ValidarMesEAno(ano, mes);

        if (_bus.Notify.HasNotifications())
            return new List<ObterSaidasMensaisViewModel>();

        var saidas = _repository.Query<Saida>(x =>  x.MercadoriaId.Equals(mercadoriaId) &&
                                                    x.DataDaSaida.Year == ano &&
                                                    x.DataDaSaida.Month == mes,
                                                    includes: y => y.Include(i => i.Mercadoria)).ToList();

        return _mapper.Map<IEnumerable<ObterSaidasMensaisViewModel>>(saidas);
        ;
    }
}