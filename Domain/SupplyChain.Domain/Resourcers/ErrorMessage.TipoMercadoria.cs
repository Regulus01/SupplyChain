namespace SupplyChain.Domain.Resourcers;

public partial class ErrorMessage
{
    public static (string Code, string Message) TIP_MERCADORIA_NAO_EXISTE =
        ("Tip00x1", "O Tipo de mercadoria informado não existe");
}