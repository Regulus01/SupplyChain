using SupplyChain.Domain.Interface;

namespace SupplyChain.Domain.Bus;

public class Bus
{
    public INotify Notify { get; private set; }

    public Bus(INotify notify)
    {
        Notify = notify;
    }
}