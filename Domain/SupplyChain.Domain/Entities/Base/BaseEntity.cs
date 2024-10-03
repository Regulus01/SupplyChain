namespace SupplyChain.Domain.Entities.Base;

public abstract class BaseEntity
{
    public Guid Id { get; private set; }
    public DateTimeOffset DataDeCriacao { get; private set; }
    public DateTimeOffset DataDeAlteracao { get; private set; }

    public void AlterarDataDeCriacao(DateTimeOffset date)
    {
        DataDeCriacao = date;
    }

    public void AlterarDataDeAlteracao(DateTimeOffset date)
    {
        DataDeAlteracao = date;
    }

    public abstract (bool IsValid, IDictionary<string, string> Erros) Validate();
}