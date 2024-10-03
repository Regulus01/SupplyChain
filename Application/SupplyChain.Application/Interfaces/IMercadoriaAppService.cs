using SupplyChain.Application.ValueObjects.Dto;
using SupplyChain.Application.ValueObjects.Dto.Mercadoria;
using SupplyChain.Application.ValueObjects.ViewModels;
using SupplyChain.Application.ValueObjects.ViewModels.Mercadoria;

namespace SupplyChain.Application.Interfaces;

public interface IMercadoriaAppService
{
    /// <summary>
    /// Responsável por inserir uma mercadoria no sistema
    /// </summary>
    /// <param name="dto">Dados necessários para a criação</param>
    /// <returns>Mercadoria criada</returns>
    CriarMercadoriaViewModel? CriarMercadoria(CriarMercadoriaDto dto);
}