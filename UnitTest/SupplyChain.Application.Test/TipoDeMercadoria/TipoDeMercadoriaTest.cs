using Moq;
using SupplyChain.Application.Interfaces;
using SupplyChain.Domain.Interface.Repository;

namespace SupplyChain.Application.Test.TipoDeMercadoria;

public class TipoDeMercadoriaTest : IClassFixture<TipoDeMercadoriaFixture>
{
    private readonly TipoDeMercadoriaFixture _fixture;
    private readonly ITipoDeMercadoriaAppService _appService;

    public TipoDeMercadoriaTest(TipoDeMercadoriaFixture fixture)
    {
        _fixture = fixture;
        _appService = _fixture.GetAppServiceFixture();
    }
    
    [Fact(DisplayName = "Create_TaskList_Success")]
    public void Create_TaskList_Success()
    {
        //Arrange
        var dto = Factory.GerarCriarTipoDeMercadoriaDto("Tipo de mercadoria");
        
        //Setup
        _fixture.SetupSaveChanges<ITipoDeMercadoriaRepository>();
        
        //Act
        var response = _appService.CriarTipoDeMercadoria(dto);

        //Assert
        Assert.Equal(response.Nome, dto.Nome);
        
        _fixture.NeverNotifications();

        _fixture.Mocker.GetMock<ITipoDeMercadoriaRepository>()
            .Verify(x => x.Add(It.IsAny<Domain.Entities.TipoDeMercadoria>()), Times.Once);
        
        _fixture.Mocker.GetMock<ITipoDeMercadoriaRepository>()
            .Verify(x => x.SaveChanges(), Times.Once);
    }
}