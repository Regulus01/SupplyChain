namespace SupplyChain.Application.ValueObjects.Dto.Estoque;

public class CadastrarEstoqueDto
{
    /// <summary>
    /// Local que estára disponível o estoque
    /// </summary>
    public string Local { get;  set; }
    
    /// <summary>
    /// Mercadoria ao qual o estoque pertence
    /// </summary>
    public Guid MercadoriaId { get;  set; }
}