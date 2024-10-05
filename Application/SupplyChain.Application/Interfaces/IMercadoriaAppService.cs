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
    /// Obtém as mercadorias, com a possíbilidade de paginação
    /// </summary>
    /// <param name="skip">Número de registros a serem ignorados</param>
    /// <param name="take">Número máximo de registros a serem retornados</param>
    /// <returns>Mercadorias</returns>
    IEnumerable<ObterMercadoriaViewModel> ObterMercadorias(int? skip = null, int? take = null);
}