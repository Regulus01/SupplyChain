using System.ComponentModel.DataAnnotations;

namespace SupplyChain.Application.ValueObjects.Dto.Estoque;

public class CadastrarEntradaDto
{
    /// <summary>
    /// Quantidades de mercadoria
    /// </summary>
    [Required]
    public int Quantidade { get; set; }
    
    /// <summary>
    /// Local da entrada
    /// </summary>
    [Required]
    public string Local { get; set; }
    
    /// <summary>
    /// Data da entrada
    /// </summary>
    [Required]
    public DateTime DataDaEntrada { get; set; }
    
    /// <summary>
    /// Id da mercadoria
    /// </summary>
    [Required]
    public Guid MercadoriaId { get; set; }
}