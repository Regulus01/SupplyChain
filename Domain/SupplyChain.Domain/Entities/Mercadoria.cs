using SupplyChain.Domain.Entities.Base;

namespace SupplyChain.Domain.Entities;

public class Mercadoria : BaseEntity
{
    public string NumeroDeRegistro { get; private set; }
    public string Nome { get; private set; }
    public string Fabricante { get; private set; }
    public string Descricao { get; private set; }
    
    public Guid TipoMercadoriaId { get; private set; }
    public TipoDeMercadoria TipoDeMercadoria { get; private set; }
    public virtual IEnumerable<Entrada> Entradas { get; private set; }
    public virtual IEnumerable<Saida> Saidas { get; private set; }

    public Mercadoria(string numeroDeRegistro, string nome, string fabricante, string descricao, Guid tipoMercadoriaId)
    {
        NumeroDeRegistro = numeroDeRegistro;
        Nome = nome;
        Fabricante = fabricante;
        Descricao = descricao;
        TipoMercadoriaId = tipoMercadoriaId;
    }
    
    public override (bool IsValid, IDictionary<string, string> Erros) Validate()
    {
        throw new NotImplementedException();
    }
}