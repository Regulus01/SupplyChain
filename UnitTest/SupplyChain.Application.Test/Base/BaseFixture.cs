using Moq;
using Moq.AutoMock;
using SupplyChain.Domain.Interface.Base;
using SupplyChain.Domain.Interface.Notification;

namespace SupplyChain.Application.Test.Base;

public class BaseFixture
{
    public AutoMocker Mocker { get; protected set; }

    public void SetupHasNotification(bool hasNotification)
    {
        Mocker.GetMock<INotify>()
            .Setup(x => x.HasNotifications())
            .Returns(hasNotification);
    }

    public void SetupSaveChanges<T>(bool success = true) where T : class, IBaseRepository
    {
        Mocker.GetMock<T>()
            .Setup(x => x.SaveChanges())
            .Returns(success);
    }
    
    public void NeverNotifications()
    {
        Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(It.IsAny<IDictionary<string, string>>()),
                Times.Never);

        Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);
    }
}