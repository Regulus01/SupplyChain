﻿namespace SupplyChain.Domain.Interface;

using NotificationDomain = Domain.Notification.Notification;

public interface INotify
{
    bool HasNotifications();
    IEnumerable<NotificationDomain> GetNotifications();
    void NewNotification(string key, string message);
    void NewNotification(IDictionary<string, string> erros);
}