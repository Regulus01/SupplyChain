using SupplyChain.Domain.Entities.Base;

namespace SupplyChain.Domain.Entities;

public class Saida : BaseEntity
{
    public int Quantidade { get; private set; }
    public string Local { get; private set; }
    public DateTimeOffset DataDaSaida { get; private set; }
    
    public Guid MercadoriaId { get; private set; }
    public Mercadoria Mercadoria { get; private set; }
    
    public override (bool IsValid, IDictionary<string, string> Erros) Validate()
    {
        throw new NotImplementedException();
    }
}