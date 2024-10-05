namespace SupplyChain.Application.ValueObjects.ViewModels.Mercadoria;

public class ObterMercadoriaViewModel
{
    /// <summary>
    /// Identificador único da entidade
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Nome da mercadoria
    /// </summary>
    public string Nome { get; set; }
}