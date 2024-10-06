using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SupplyChain.Application.Interfaces;
using SupplyChain.Application.ValueObjects.Dto.Estoque;
using SupplyChain.Application.ValueObjects.ViewModels.Estoque;
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

        return Response(HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Insere uma entrada em no sistema 
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

    /// <summary>
    /// Insere uma nova saída no sistema
    /// </summary>
    /// <remarks>
    /// Caso não houver disponibilidade no estoque a saída não será realizada
    /// </remarks>
    /// <param name="dto"></param>
    /// <returns>Saida criada</returns>
    [ProducesResponseType(typeof(CadastrarEntradaViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    [Route("Saida")]
    public IActionResult InserirSaidaNoEstoque([FromBody] CadastrarSaidaDto dto)
    {
        var result = _estoqueAppService.CadastrarSaida(dto);

        return Response(HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Obtem os locais do estoque da mercadoria
    /// </summary>
    /// <param name="mercadoriaId">Id da mercadoria</param>
    /// <param name="skip">Número de registros a serem ignorados</param>
    /// <param name="take">Número máximo de registros a serem retornados</param>
    /// <returns>Lista com locais</returns>
    [ProducesResponseType(typeof(ObterLocaisDeEstoqueViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    [Route("{mercadoriaId:guid}/Locais")]
    public IActionResult ListarLocaisDoEstoque([FromRoute] Guid mercadoriaId, [FromQuery] int? skip = null,
        [FromQuery] int? take = null)
    {
        var result = _estoqueAppService.ObterLocaisDoEstoqueDaMercadoria(mercadoriaId, skip, take);

        return Response(HttpStatusCode.OK, result);
    }

    /// <summary>
    ///  Gera um relatório anual com o total de entradas e saídas mensais de uma mercadoria específica.
    /// </summary>
    /// <param name="mercadoriaId">O identificador único da mercadoria.</param>
    /// <param name="ano">O ano do qual o relatório será gerado.</param>
    /// <returns>
    /// Dicionário onde a chave é o nome do mês e o valor é um <see cref="TotaisMensaisViewModel"/> 
    /// contendo os totais de entradas e saídas mensais da mercadoria especificada.
    /// </returns>
    [ProducesResponseType(typeof(TotaisMensaisViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    [Route("Relatorio/{mercadoriaId:guid}/{ano:int}")]
    public IActionResult ObterRelatorioAnual([FromRoute] [Required] Guid mercadoriaId, [FromRoute] [Required] int ano)
    {
        var result = _estoqueAppService.ObterRelatorioAnual(mercadoriaId, ano);

        return Response(HttpStatusCode.OK, result);
    }

    /// <summary>
    /// Obtem as entradas mensais de uma mercadoria e um ano especifico
    /// </summary>
    /// <param name="mercadoriaId">Id da mercadoria</param>
    /// <param name="mes">Mes</param>
    /// <param name="ano">Ano</param>
    /// <returns>Lista com as entradas do mes e ano do filtro</returns>
    [ProducesResponseType(typeof(ObterEntradasMensaisViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    [Route("Entradas/{mercadoriaId:guid}/{ano:int}/{mes:int}")]
    public IActionResult ObterEntradasMensais([FromRoute] [Required] Guid mercadoriaId, [FromRoute] [Required] int ano, 
        [FromRoute] [Required] int mes)
    {
        var result = _estoqueAppService.ObterEntradasMensais(mercadoriaId, ano, mes);

        return Response(HttpStatusCode.OK, result);
    }

    /// <summary>
    /// Obtem as saidas mensais de uma mercadoria e um ano especifico
    /// </summary>
    /// <param name="mercadoriaId">Id da mercadoria</param>
    /// <param name="mes">Mes</param>
    /// <param name="ano">Ano</param>
    /// <returns>Lista com as saidas do mes e ano do filtro</returns>
    [ProducesResponseType(typeof(ObterSaidasMensaisViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    [Route("Saidas/{mercadoriaId:guid}/{ano:int}/{mes:int}")]
    public IActionResult ObterSaidasMensais([FromRoute] [Required] Guid mercadoriaId, [FromRoute] [Required] int ano, 
        [FromRoute] [Required] int mes)
    {
        var result = _estoqueAppService.ObterSaidasMensais(mercadoriaId, ano, mes);

        return Response(HttpStatusCode.OK, result);
    }
}