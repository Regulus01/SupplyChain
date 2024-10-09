using SupplyChain.Domain.Entities.Base;
using SupplyChain.Domain.Resourcers;
using NotificationDomain = SupplyChain.Domain.Notification.Notification;

namespace SupplyChain.Domain.Entities;

public class TipoDeMercadoria : BaseEntity
{
    public string Nome { get; set; }
    
    public virtual IEnumerable<Mercadoria> Mercadorias { get; set; }

    public TipoDeMercadoria(string nome)
    {
        Nome = nome;
    }
    
    public override (bool IsValid, IEnumerable<NotificationDomain> Erros) Validate()
    {
        var erros = new List<NotificationDomain>();
        
        if (string.IsNullOrWhiteSpace(Nome))
        {
            erros.Add(new NotificationDomain(ErrorMessage.TIP_NOME_VAZIO.Code, ErrorMessage.TIP_NOME_VAZIO.Message));
        }
        
        return (erros.Count == 0, erros);
    }
}