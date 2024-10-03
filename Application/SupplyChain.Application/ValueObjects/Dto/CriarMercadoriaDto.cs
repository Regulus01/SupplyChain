using System.ComponentModel.DataAnnotations;
using SupplyChain.Domain.Resourcers;

namespace SupplyChain.Application.ValueObjects.Dto;

public record CriarMercadoriaDto
{
    /// <summary>
    /// Número do registro no formato 1234 ou letras e números, sem espaços
    /// </summary>
    [Required()]
    public string NumeroDeRegistro { get; set; }
    
    /// <summary>
    /// Nome da mercadoria
    /// </summary>
    [Required]
    public string Nome { get; set; }
    
    /// <summary>
    /// Fabricante da mercadoria
    /// </summary>
    [Required]
    public string Fabricante { get; set; }
    
    /// <summary>
    /// Descrição da mercadoria
    /// </summary>
    [Required]
    public string Descricao { get; set; }
    
    /// <summary>
    /// Id do tipo da mercadoria
    /// </summary>
    [Required]
    public Guid TipoMercadoriaId { get; set; }
}