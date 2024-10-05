using SupplyChain.Domain.Interface.Notification;

namespace SupplyChain.Domain.Interface.Bus;

public interface IBus
{
    INotify Notify { get; }
}