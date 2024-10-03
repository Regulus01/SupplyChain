using SupplyChain.Application.ValueObjects.Dto;
using SupplyChain.Application.ValueObjects.ViewModels;

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