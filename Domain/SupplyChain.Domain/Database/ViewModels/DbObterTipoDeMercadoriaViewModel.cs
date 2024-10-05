namespace SupplyChain.Domain.Database.ViewModels;

public class DbObterTipoDeMercadoriaViewModel
{
    
    /// <summary>
    /// Identificador único da entidade
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Nome do tipo de mercadoria
    /// </summary>
    public string Tip_Nome { get; set; }
}