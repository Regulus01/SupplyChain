namespace SupplyChain.Domain.Database.ViewModels;

public class DbObterMercadoriaViewModel
{
    /// <summary>
    /// Identificador único da entidade
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Nome da mercadoria
    /// </summary>
    public string Mer_Nome { get; set; }
}