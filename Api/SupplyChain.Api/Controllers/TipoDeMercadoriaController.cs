using System.Net;
using Microsoft.AspNetCore.Mvc;
using SupplyChain.Application.Interfaces;
using SupplyChain.Application.ValueObjects.Dto.TipoDeMercadoria;
using SupplyChain.Application.ValueObjects.ViewModels.TipoDeMercadoria;
using SupplyChain.Domain.Interface.Notification;
using SupplyChain.Infra.CrossCutting.Controller;

namespace SupplyChain.Api.Controllers;

[Route("api/v1/TipoDeMercadoria")]
public class TipoDeMercadoriaController : BaseController
{
    private readonly ITipoDeMercadoriaAppService _appService;
    
    public TipoDeMercadoriaController(INotify notify, ITipoDeMercadoriaAppService appService) : base(notify)
    {
        _appService = appService;
    }
    
    /// <summary>
    /// Insere um tipo de mercadoria no sistema
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Mercadoria criada</returns>
    [ProducesResponseType(typeof(CriarTipoDeMercadoriaViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public IActionResult Criar([FromBody] CriarTipoDeMercadoriaDto dto)
    {
        var result = _appService.CriarTipoDeMercadoria(dto);

        return Response(HttpStatusCode.Created, result);
    }
    
}