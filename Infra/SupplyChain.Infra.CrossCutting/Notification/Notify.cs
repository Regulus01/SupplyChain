﻿using SupplyChain.Domain.Interface;
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

    public void NewNotification(IDictionary<string, string> erros)
    {
        foreach (var erro in erros)
        {
            _notifications.Add(new NotificationDomain(erro.Key, erro.Value));
        }
    }
}