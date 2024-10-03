using SupplyChain.Application.ValueObjects.ViewModels.Base;

namespace SupplyChain.Application.ValueObjects.ViewModels.Mercadoria;

public class CriarMercadoriaViewModel : BaseViewModel
{
    public string NumeroDeRegistro { get; private set; }
    public string Nome { get; private set; }
    public string Fabricante { get; private set; }
    public string Descricao { get; private set; }
    public string TipoDeMercadoria { get; private set; }
}