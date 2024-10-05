using SupplyChain.Application.ValueObjects.ViewModels.Base;

namespace SupplyChain.Application.ValueObjects.ViewModels.Estoque;

public class CadastrarSaidaViewModel : BaseViewModel
{
    public int Quantidade { get; set; }
    public string Local { get; set; }
    public DateTimeOffset DataDaSaida { get; set; }
}