using SupplyChain.Application.ValueObjects.Dto.Estoque;
using SupplyChain.Application.ValueObjects.Dto.Mercadoria;
using SupplyChain.Application.ValueObjects.ViewModels.Mercadoria;

namespace SupplyChain.Application.Interfaces;

public interface IMercadoriaAppService
{
    /// <summary>
    /// Insere uma mercadoria no sistema
    /// </summary>
    /// <param name="dto">Dados necessários para a criação</param>
    /// <returns>Mercadoria criada</returns>
    CriarMercadoriaViewModel? CriarMercadoria(CriarMercadoriaDto dto);

    /// <summary>
    /// Insere uma saída de uma mercado no sistema
    /// </summary>
    /// <remarks>
    ///     Não será possível cadastrar uma saída caso não tenha estoque na localidade informada
    /// </remarks>
    /// <param name="dto"></param>
    /// <returns></returns>
    CadastrarSaidaViewModel? CadastrarSaida(CadastrarSaidaDto dto);
}