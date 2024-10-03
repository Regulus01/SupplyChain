namespace SupplyChain.Infra.CrossCutting.Notification.Response;

using NotificationDomain = SupplyChain.Domain.Notification.Notification;

public class ErrorResponse
{
    public bool Sucess { get; private set; }
    public DateTimeOffset Time { get; private set; }
    public IEnumerable<NotificationDomain> Error { get; private set; }

    public ErrorResponse(IEnumerable<NotificationDomain> error)
    {
        Sucess = false;
        Time = DateTimeOffset.UtcNow;
        Error = error;
    }
}