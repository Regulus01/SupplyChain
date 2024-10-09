using SupplyChain.Domain.Interface.Notification;
using NotificationDomain = SupplyChain.Domain.Notification.Notification;

namespace SupplyChain.Infra.CrossCutting.Notification;

public class Notify : INotify
{
    private List<NotificationDomain> _notifications = [];

    public IEnumerable<NotificationDomain> GetNotifications()
    {
        return _notifications.Where(not => not.GetType() == typeof(NotificationDomain)).ToList();
    }

    public bool HasNotifications()
    {
        return GetNotifications().Any();
    }

    public void NewNotification(string key, string message)
    {
        _notifications.Add(new NotificationDomain(key, message));
    }

    public void NewNotification(IEnumerable<NotificationDomain> erros)
    {
        foreach (var erro in erros)
        {
            _notifications.Add(new NotificationDomain(erro.Code, erro.Message));
        }
    }
}