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
}