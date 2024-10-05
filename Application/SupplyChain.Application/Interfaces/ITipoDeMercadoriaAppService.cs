using SupplyChain.Application.ValueObjects.Dto.TipoDeMercadoria;
using SupplyChain.Application.ValueObjects.ViewModels.TipoDeMercadoria;

namespace SupplyChain.Application.Interfaces;

public interface ITipoDeMercadoriaAppService
{
    /// <summary>
    /// Cria um tipo de mercadoria
    /// </summary>
    /// <param name="dto">Dados necessários para a criação</param>
    /// <returns>Tipo de mercadoria criada</returns>
    CriarTipoDeMercadoriaViewModel? CriarTipoDeMercadoria(CriarTipoDeMercadoriaDto dto);

    /// <summary>
    /// Obtém os tipos de mercadoria, com a possíbilidade de paginação
    /// </summary>
    /// <param name="skip">Número de registros a serem ignorados</param>
    /// <param name="take">Número máximo de registros a serem retornados</param>
    /// <returns>Tipos de mercadoria</returns>
    IEnumerable<ObterTipoDeMercadoriaViewModel> ObterTipoDeMercadoria(int? skip = null, int? take = null);
}