using SupplyChain.Application.ValueObjects.Dto.Estoque;
using SupplyChain.Application.ValueObjects.Dto.Mercadoria;
using SupplyChain.Application.ValueObjects.ViewModels.Estoque;
using SupplyChain.Application.ValueObjects.ViewModels.Mercadoria;

namespace SupplyChain.Application.Interfaces;

public interface IEstoqueAppService
{
    /// <summary>
    ///  Cadastra um estoque no sistema
    /// </summary>
    /// <param name="dto">Dados necessários para o cadastro</param>
    /// <returns>Estoque Criado</returns>
    CadastrarEstoqueViewModel? CadastrarEstoque(CadastrarEstoqueDto dto);
    
    /// <summary>
    ///  Cadastra uma entrada no sistema
    /// </summary>
    /// <remarks>
    /// A entrada registrada será refletida na quantidade do estoque atual
    /// </remarks>
    /// <param name="dto">Dados necessários para o cadastro</param>
    /// <returns>Estoque Criado</returns>
    CadastrarEntradaViewModel? CadastrarEntrada(CadastrarEntradaDto dto);
    
    /// <summary>
    /// Insere uma saída de um estoque no sistema
    /// </summary>
    /// <remarks>
    ///     Não será possível cadastrar uma saída caso não tenha estoque na localidade informada
    /// </remarks>
    /// <param name="dto">Dados necessários para a saida</param>
    /// <returns>Saida cadastrada</returns>
    CadastrarSaidaViewModel? CadastrarSaida(CadastrarSaidaDto dto);

    /// <summary>
    /// Obtém os locais que a mercadoria possui estoque, como possibilidade de paginação
    /// </summary>
    /// <param name="mercadoriaId">Id da mercadoria</param>
    /// <param name="skip">Número de registros a serem ignorados</param>
    /// <param name="take">Número máximo de registros a serem retornados</param>
    /// <returns>Locais da mercadoria</returns>
    IEnumerable<ObterLocaisDeEstoqueViewModel> ObterLocaisDoEstoqueDaMercadoria(Guid mercadoriaId,
        int? skip = null, int? take = null);

    /// <summary>
    /// Gera um relatório anual com o total de entradas e saídas mensais de uma mercadoria específica.
    /// </summary>
    /// <param name="mercadoriaId">O identificador único da mercadoria .</param>
    /// <param name="ano">O ano do qual o relatório será gerado.</param>
    /// <returns>
    /// Dicionário onde a chave é o nome do mês e o valor é um <see cref="TotaisMensaisViewModel"/> 
    /// contendo os totais de entradas e saídas mensais da mercadoria especificada.
    /// </returns>
    Dictionary<string, TotaisMensaisViewModel>? ObterRelatorioAnual(Guid mercadoriaId, int ano);

    /// <summary>
    /// Obtem as entradas do mes e ano escolhidos de uma mercadoria
    /// </summary>
    /// <param name="mercadoriaId">Id da mercadoria</param>
    /// <param name="ano">Ano para filtragem</param>
    /// <param name="mes">mes para filtragem</param>
    /// <returns>Lista com entradas</returns>
    IEnumerable<ObterEntradasMensaisViewModel> ObterEntradasMensais(Guid mercadoriaId, int ano, int mes);
    
    /// <summary>
    /// Obtem as saidas do mes e ano escolhidos de uma mercadoria
    /// </summary>
    /// <param name="mercadoriaId">Id da mercadoria</param>
    /// <param name="ano">Ano para filtragem</param>
    /// <param name="mes">mes para filtragem</param>
    /// <returns>Lista com saidas</returns>
    IEnumerable<ObterSaidasMensaisViewModel> ObterSaidasMensais(Guid mercadoriaId, int ano, int mes);

}