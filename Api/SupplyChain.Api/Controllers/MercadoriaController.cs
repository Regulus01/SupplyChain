using System.Net;
using Microsoft.AspNetCore.Mvc;
using SupplyChain.Application.Interfaces;
using SupplyChain.Application.ValueObjects.Dto.Mercadoria;
using SupplyChain.Application.ValueObjects.ViewModels.TipoDeMercadoria;
using SupplyChain.Domain.Interface.Notification;
using SupplyChain.Infra.CrossCutting.Controller;

namespace SupplyChain.Api.Controllers;

[Route("api/v1/Mercadoria/")]
public class MercadoriaController : BaseController
{
    private readonly IMercadoriaAppService _mercadoriaAppService;
    
    public MercadoriaController(INotify notify, IMercadoriaAppService mercadoriaAppService) : base(notify)
    {
        _mercadoriaAppService = mercadoriaAppService;
    }
        
    /// <summary>
    /// Insere uma mercadoria no sistema
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Mercadoria criada</returns>
    [ProducesResponseType(typeof(CriarTipoDeMercadoriaViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public IActionResult Criar([FromBody] CriarMercadoriaDto dto)
    {
        var result = _mercadoriaAppService.CriarMercadoria(dto);

        return Response(HttpStatusCode.Created, result);
    }
}