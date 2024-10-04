using SupplyChain.Application.ValueObjects.ViewModels.Base;

namespace SupplyChain.Application.ValueObjects.ViewModels.Estoque;

public class CadastrarEstoqueViewModel : BaseViewModel
{
    /// <summary>
    /// Local que estára disponível o estoque
    /// </summary>
    public string Local { get;  set; }
    
    /// <summary>
    /// Quantidade do estoque
    /// </summary>
    public int Quantidade { get;  set; }
    
    /// <summary>
    /// Mercadoria ao qual o estoque pertence
    /// </summary>
    public Guid MercadoriaId { get;  set; }
}