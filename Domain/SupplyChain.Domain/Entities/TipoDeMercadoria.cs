using SupplyChain.Domain.Entities.Base;

namespace SupplyChain.Domain.Entities;

public class TipoDeMercadoria : BaseEntity
{
    public string Nome { get; set; }
    
    public virtual IEnumerable<Mercadoria> Mercadorias { get; set; }

    public TipoDeMercadoria(string nome)
    {
        Nome = nome;
    }
    
    public override (bool IsValid, IDictionary<string, string> Erros) Validate()
    {
        throw new NotImplementedException();
    }
}