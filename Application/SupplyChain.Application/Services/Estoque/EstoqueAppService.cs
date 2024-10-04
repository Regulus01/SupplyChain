using AutoMapper;
using SupplyChain.Application.Interfaces;
using SupplyChain.Application.ValueObjects.Dto.Estoque;
using SupplyChain.Application.ValueObjects.Dto.Mercadoria;
using SupplyChain.Application.ValueObjects.ViewModels.Estoque;
using SupplyChain.Application.ValueObjects.ViewModels.Mercadoria;
using SupplyChain.Domain.Bus;
using SupplyChain.Domain.Interface.Repository;
using EstoqueDomain = SupplyChain.Domain.Entities.Estoque;

namespace SupplyChain.Application.Services.Estoque;

public partial class EstoqueAppService : IEstoqueAppService
{
    private readonly IEstoqueRepository _repository;
    private readonly Bus _bus;
    private readonly IMapper _mapper;

    public EstoqueAppService(IEstoqueRepository repository, IMapper mapper, Bus bus)
    {
        _repository = repository;
        _mapper = mapper;
        _bus = bus;
    }

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
        
        if(estoque != null)
            _repository.Update(estoque);
        
        if (!SaveChanges())
            return null;
        
        return _mapper.Map<CadastrarEntradaViewModel>(entrada, options =>
        {
            options.Items["NomeDaMercadoria"] = mercadoria?.Nome;
        });
    }
    
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
        
        if(estoque != null)
            _repository.Update(estoque);
        
        if (!SaveChanges())
            return null;
        
        return _mapper.Map<CadastrarSaidaViewModel>(saida);
    }
}