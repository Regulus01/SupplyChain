using SupplyChain.Application.ValueObjects.ViewModels.Base;

namespace SupplyChain.Application.ValueObjects.ViewModels.TipoDeMercadoria;

public class CriarTipoDeMercadoriaViewModel : BaseViewModel
{
    /// <summary>
    /// Nome do tipo de mercadoria
    /// </summary>
    public string Nome { get; set; }
}