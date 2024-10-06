using SupplyChain.Application.ValueObjects.Dto.TipoDeMercadoria;

namespace SupplyChain.Application.Test.TipoDeMercadoria;

public class Factory
{
    public static CriarTipoDeMercadoriaDto GerarCriarTipoDeMercadoriaDto(string nome = "um tipo")
    {
        return new CriarTipoDeMercadoriaDto
        {
            Nome = nome
        };
    }
}