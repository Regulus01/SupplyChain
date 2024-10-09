using SupplyChain.Domain.Entities.Base;
using NotificationDomain = SupplyChain.Domain.Notification.Notification;

namespace SupplyChain.Domain.Entities;

public class Estoque : BaseEntity
{
    public string Local { get; private set; }
    public int Quantidade { get; private set; }
    
    public Guid MercadoriaId { get; private set; }
    public Mercadoria Mercadoria { get; private set; }

    public Estoque(string local, Guid mercadoriaId)
    {
        Local = local;
        Quantidade = 0;
        MercadoriaId = mercadoriaId;
    }

    public void AdicionarEstoque(int quantidade)
    {
        Quantidade += quantidade;
    }

    public void RemoverEstoque(int quantidade)
    {
        Quantidade -= quantidade;
    }
    
    public override (bool IsValid, IEnumerable<NotificationDomain> Erros) Validate()
    {
        var erros = new List<NotificationDomain>();
        
        if(Guid.Empty == MercadoriaId)
            erros.Add(new NotificationDomain("Erro", "A mercadoria deve ser informada."));
        
        return (erros.Count == 0, erros); 
    }
}