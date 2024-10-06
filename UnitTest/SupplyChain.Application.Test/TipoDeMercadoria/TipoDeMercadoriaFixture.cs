using Moq.AutoMock;
using SupplyChain.Application.Mapper;
using SupplyChain.Application.Services.TipoDeMercadoria;
using SupplyChain.Application.Test.Base;

namespace SupplyChain.Application.Test.TipoDeMercadoria;

public class TipoDeMercadoriaFixture : BaseFixture
{
    public TipoDeMercadoriaAppService GetAppServiceFixture()
    {
        Mocker = new AutoMocker();
        
        Mocker.Use(MappingConfiguration.RegisterMappings().CreateMapper());

        return Mocker.CreateInstance<TipoDeMercadoriaAppService>();
    }
}