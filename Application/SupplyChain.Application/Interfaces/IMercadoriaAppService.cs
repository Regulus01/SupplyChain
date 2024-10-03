﻿using SupplyChain.Application.ValueObjects.Dto.Mercadoria;
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
    ///  Insere a entrada de uma mercadoria no sistema
    /// </summary>
    /// <param name="dto">Dados ncessários para o cadastro</param>
    /// <returns>Entrada Criada</returns>
    CadastrarEntradaViewModel? CadastrarEntrada(CadastrarEntradaDto dto);
}