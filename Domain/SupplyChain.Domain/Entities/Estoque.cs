using SupplyChain.Domain.Entities.Base;

namespace SupplyChain.Domain.Entities;

public class Estoque : BaseEntity
{
    public string Local { get; set; }
    public int Quantidade { get; set; }
    
    public Guid MercadoriaId { get; set; }
    public Mercadoria Mercadoria { get; set; }

    public override (bool IsValid, IDictionary<string, string> Erros) Validate()
    {
        var erros = new Dictionary<string, string>();
        
        return (erros.Count == 0, erros); 
    }
}