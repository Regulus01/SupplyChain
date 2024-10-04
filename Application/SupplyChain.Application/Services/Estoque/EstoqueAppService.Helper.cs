using SupplyChain.Application.ValueObjects.Dto.Estoque;
using SupplyChain.Application.ValueObjects.Dto.Mercadoria;
using SupplyChain.Domain.Entities;
using SupplyChain.Domain.Entities.Base;
using SupplyChain.Domain.Resourcers;
using EstoqueDomain = SupplyChain.Domain.Entities.Estoque;
using MercadoriaDomain = SupplyChain.Domain.Entities.Mercadoria;

namespace SupplyChain.Application.Services.Estoque;

public partial class EstoqueAppService
{
    /// <summary>
    /// Valida se existe um estoque no local informado
    /// </summary>
    /// <remarks>
    ///  Retorna o estoque como variável de saída caso exista
    /// </remarks>
    /// <param name="local">Local que o estoque se encontra</param>
    /// <param name="mercadoriaId">Id da mercadoria</param>
    /// <param name="estoque">Variável de saida para o estoque existente</param>
    /// <returns>Retorna <c>false</c> se o local não existe e <c>true</c> caso exista.</returns>
    private bool ValidarLocalDoEstoqueExistente(string local, Guid mercadoriaId, out EstoqueDomain? estoque)
    {
        estoque = _repository.Query<EstoqueDomain>(x => x.Local.ToLower().Equals(local.ToLower()) && 
                                                        x.MercadoriaId == mercadoriaId)
                             .FirstOrDefault();

        if (estoque == null)
        {
            _bus.Notify.NewNotification("Erro", "Não existe um estoque ou mercadoria para o local informado.");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Valida se existe uma mercadoria existente para o mercadoriaId informado
    /// </summary>
    /// <param name="mercadoriaId">Id da mercadoria</param>
    /// <param name="mercadoria">Váriavel de saída para a mercadoria, caso exista</param>
    /// <returns>Retorna <c>false</c> se mercadoria não existe e <c>true</c> caso exista.</returns>
    private bool ValidarMercadoriaExistente(Guid mercadoriaId, out MercadoriaDomain? mercadoria)
    {
        mercadoria = _repository.Query<MercadoriaDomain>(x => x.Id.Equals(mercadoriaId))
                                .FirstOrDefault();

        if (mercadoria == null)
        {
            _bus.Notify.NewNotification("Erro", "A mercadoria informada não existe.");
            return false;
        }

        return true;
    }

    /// <summary>
    /// A partir de um dto de castrar estoque, cria um estoque
    /// </summary>
    /// <param name="dto">Dados necessários para criação</param>
    /// <returns>Estoque</returns>
    private EstoqueDomain CriarEstoque(CadastrarEstoqueDto dto)
    {
        var estoque = new EstoqueDomain(dto.Local, dto.MercadoriaId);

        return estoque;
    }
    
    /// <summary>
    /// Cria uma entrada a partir de um dto
    /// </summary>
    /// <param name="dto">Dados necessários para criação</param>
    /// <returns>Entidade entrada</returns>
    private Entrada CriarEntrada(CadastrarEntradaDto dto)
    {
        var dataDeEntrada = AlterarDataParaUtc(dto.DataDaEntrada);
        
        var entrada = new Entrada(dto.Quantidade, dto.Local, dataDeEntrada, dto.MercadoriaId);
        return entrada;
    }
    
    /// <summary>
    /// Altera uma data para o formato utc −3 hr
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private DateTime AlterarDataParaUtc(DateTime data)
    {
        return data.AddHours(-3);  
    }
    
    /// <summary>
    /// Valida se uma mercadoria é valida para a inserção no sistema
    /// </summary>
    /// <param name="entity">Entidade a ser validada </param>
    /// <returns>Retorna <c>false</c> se a entity está inválida e <c>true</c> caso válida.</returns>
    private bool Validar(BaseEntity entity)
    {
        var validacao = entity.Validate();
        
        if (!validacao.IsValid)
        {
            _bus.Notify.NewNotification(validacao.Erros);
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Valida o estoque atual
    /// </summary>
    /// <param name="dto">Dto do tipo <see cref="CadastrarSaidaDto"/> utilizado para as validações</param>
    /// <param name="estoque"><see cref="EstoqueDomain"/> a ser validado</param>
    /// <returns>Se valido <c>true</c> caso contrário <c>false</c></returns>
    private bool ValidarEstoque(CadastrarSaidaDto dto, EstoqueDomain? estoque)
    {
        if (estoque is { Quantidade: <= 0 } || estoque?.Quantidade < dto.Quantidade)
        {
            _bus.Notify.NewNotification("Erro", "A quantidade do estoque não é o suficiente para a retirada");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Cria uma entidade do tipo <see cref="Saida"/>
    /// </summary>
    /// <param name="dto">Dados do tipo <see cref="CadastrarSaidaDto"/> para a criação da entidade</param>
    /// <returns>Entidade do tipo <see cref="Saida"/></returns>
    private Saida CriarSaida(CadastrarSaidaDto dto)
    {
        var dataDeSaida = AlterarDataParaUtc(dto.DataDaSaida);

        var saida = new Saida(dto.Quantidade, dto.Local, dataDeSaida, dto.MercadoriaId);
        
        return saida;
    }
    
    /// <summary>
    /// Salva no banco de dados, caso haja erro, gera uma notificação
    /// </summary>
    /// <returns><c>true</c> caso não haja erros, caso contrário <c>false</c> </returns>
    private bool SaveChanges()
    {
        if (!_repository.SaveChanges())
        {
            _bus.Notify.NewNotification(ErrorMessage.ERRO_SALVAR.Code, 
                                        ErrorMessage.ERRO_SALVAR.Message);
            return false;
        }

        return true;
    }

}