using System.ComponentModel.DataAnnotations;

namespace SupplyChain.Application.ValueObjects.Dto.TipoDeMercadoria;

public class CriarTipoDeMercadoriaDto
{
    /// <summary>
    /// Nome do tipo de mercadoria
    /// </summary>
    [Required]
    public string Nome { get; set; }
}