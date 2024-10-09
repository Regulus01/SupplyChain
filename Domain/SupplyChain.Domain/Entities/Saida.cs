using SupplyChain.Domain.Entities.Base;
using NotificationDomain = SupplyChain.Domain.Notification.Notification;

namespace SupplyChain.Domain.Entities;

public class Saida : BaseEntity
{
    public int Quantidade { get; private set; }
    public string Local { get; private set; }
    public DateTimeOffset DataDaSaida { get; private set; }
    
    public Guid MercadoriaId { get; private set; }
    public Mercadoria Mercadoria { get; private set; }

    public Saida(int quantidade, string local, DateTimeOffset dataDaSaida, Guid mercadoriaId)
    {
        Quantidade = quantidade;
        Local = local;
        DataDaSaida = dataDaSaida;
        MercadoriaId = mercadoriaId;
    }
    
    public override (bool IsValid, IEnumerable<NotificationDomain> Erros) Validate()
    {
        var erros = new List<NotificationDomain>();
        
        return (erros.Count == 0, erros); 
    }
}