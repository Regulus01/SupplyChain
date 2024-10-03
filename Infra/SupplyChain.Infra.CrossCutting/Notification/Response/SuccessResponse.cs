namespace SupplyChain.Infra.CrossCutting.Notification.Response;

public class SuccessResponse
{
    public bool Sucess { get; private set; }
    public DateTimeOffset Time { get; private set; }
    public object? Data { get; private set; }

    public SuccessResponse(object? data = null)
    {
        Sucess = true;
        Time = DateTimeOffset.UtcNow;
        Data = data;
    }
}