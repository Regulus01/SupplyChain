namespace SupplyChain.Application.ValueObjects.ViewModels.Base;

public class BaseViewModel
{
    public Guid Id { get; set; }
    public DateTimeOffset DataDeCriacao { get; set; }
    public DateTimeOffset DataDeAlteracao { get; set; }
}