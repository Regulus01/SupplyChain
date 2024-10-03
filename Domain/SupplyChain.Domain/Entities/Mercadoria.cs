using SupplyChain.Domain.Entities.Base;

namespace SupplyChain.Domain.Entities;

public class Mercadoria : BaseEntity
{
    public long NumeroDeRegistro { get; private set; }
    public string Nome { get; private set; }
    public string Fabricante { get; private set; }
    public string Descricao { get; private set; }
    
    public TipoDeMercadoria TipoDeMercadoria { get; private set; }
    public IEnumerable<Entrada> Entrada { get; private set; }
    
    public override (bool IsValid, IDictionary<string, string> Erros) Validate()
    {
        throw new NotImplementedException();
    }
}