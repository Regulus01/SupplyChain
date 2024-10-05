using SupplyChain.Domain.Interface.Bus;
using SupplyChain.Domain.Interface.Notification;

namespace SupplyChain.Domain.Bus;

public class Bus : IBus
{
    public INotify Notify { get; private set; }

    public Bus(INotify notify)
    {
        Notify = notify;
    }
}