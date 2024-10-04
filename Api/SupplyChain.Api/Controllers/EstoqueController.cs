using System.Net;
using Microsoft.AspNetCore.Mvc;
using SupplyChain.Application.Interfaces;
using SupplyChain.Application.ValueObjects.Dto.Estoque;
using SupplyChain.Application.ValueObjects.ViewModels.Estoque;
using SupplyChain.Application.ValueObjects.ViewModels.Mercadoria;
using SupplyChain.Domain.Interface.Notification;
using SupplyChain.Infra.CrossCutting.Controller;

namespace SupplyChain.Api.Controllers;

[Route("api/v1/Estoque/")]
public class EstoqueController : BaseController
{
    private readonly IEstoqueAppService _estoqueAppService;
    
    public EstoqueController(INotify notify, IEstoqueAppService estoqueAppService) : base(notify)
    {
        _estoqueAppService = estoqueAppService;
    }
        
    /// <summary>
    /// Cadastra um novo estoque no sistema
    /// </summary>
    /// <remarks>
    /// O estoque não será cadastrado se houver um estoque com o mesmo local já cadastrado.
    /// </remarks>
    /// <param name="dto"></param>
    /// <returns>Estoque Criado</returns>
    [ProducesResponseType(typeof(CadastrarEstoqueViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public IActionResult CadastrarEstoque([FromBody] CadastrarEstoqueDto dto)
    {
        var result = _estoqueAppService.CadastrarEstoque(dto);

        return Response(HttpStatusCode.OK, result);
    }
    
    /// <summary>
    /// Insere uma entrada num estoque 
    /// </summary>
    /// <remarks>
    /// Não será cadastrado se a mercadoria não exista ou não exista estoque no local para mercadoria informada
    /// </remarks>
    /// <param name="dto"></param>
    /// <returns>Entrada criada</returns>
    [ProducesResponseType(typeof(CadastrarEntradaViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    [Route("Entrada")]
    public IActionResult InserirEntradaEstoque([FromBody] CadastrarEntradaDto dto)
    {
        var result = _estoqueAppService.CadastrarEntrada(dto);

        return Response(HttpStatusCode.Created, result);
    }
}