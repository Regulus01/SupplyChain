namespace SupplyChain.Application.ValueObjects.ViewModels.Estoque;

public class ObterSaidasMensaisViewModel
{
    public string Local { get; private set; }
    public int Quantidade { get; private set; }
    public DateTime DataDaSaida { get; private set; }
    public string Mercadoria { get; private set; }
}