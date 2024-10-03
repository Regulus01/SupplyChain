namespace SupplyChain.Application.ValueObjects.Dto.Mercadoria;

public class CadastrarSaidaDto
{
    public int Quantidade { get; set; }
    public string Local { get; set; }
    public DateTime DataDaSaida { get; set; }
    public Guid MercadoriaId { get; set; }
}