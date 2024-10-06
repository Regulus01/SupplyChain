namespace SupplyChain.Application.ValueObjects.ViewModels.Estoque;

public class ObterEntradasMensaisViewModel
{
    public string Local { get; private set; }
    public int Quantidade { get; private set; }
    public DateTime DataDaEntrada { get; private set; }
    public string Mercadoria { get; private set; }
}