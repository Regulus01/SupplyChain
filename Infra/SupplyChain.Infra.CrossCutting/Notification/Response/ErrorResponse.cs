namespace SupplyChain.Infra.CrossCutting.Notification.Response;

using NotificationDomain = SupplyChain.Domain.Notification.Notification;

public class ErrorResponse
{
    public bool Success { get; private set; }
    public DateTimeOffset Time { get; private set; }
    public IEnumerable<NotificationDomain> Error { get; private set; }

    public ErrorResponse(IEnumerable<NotificationDomain> error)
    {
        Success = false;
        Time = DateTimeOffset.UtcNow;
        Error = error;
    }
}