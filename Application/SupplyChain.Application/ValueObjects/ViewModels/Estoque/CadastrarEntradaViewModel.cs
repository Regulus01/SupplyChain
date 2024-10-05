using SupplyChain.Application.ValueObjects.ViewModels.Base;

namespace SupplyChain.Application.ValueObjects.ViewModels.Estoque;

public class CadastrarEntradaViewModel : BaseViewModel
{
    public int Quantidade { get; set; }
    public string Local { get; set; }
    public DateTimeOffset DataDaEntrada { get; set; }
    public string NomeDaMercadoria { get; set; }
}