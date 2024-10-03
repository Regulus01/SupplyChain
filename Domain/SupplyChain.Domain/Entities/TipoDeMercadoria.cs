using SupplyChain.Domain.Entities.Base;
using SupplyChain.Domain.Resourcers;

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
        var erros = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(Nome))
        {
            erros.Add(ErrorMessage.TIP_NOME_VAZIO.Code, ErrorMessage.TIP_NOME_VAZIO.Message);
        }
        
        return (erros.Count == 0, erros);
    }
}