using FakeItEasy;
using WebShop;
using WebShop.Notifications;
using WebShop.Repository;
using WebShop.UnitOfWork;

namespace WebShopTests
{
    public class UnitOfWorkTests
    {
        private readonly WebShopDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            _context = A.Fake<WebShopDbContext>();
            _unitOfWork = new UnitOfWork(_context);

        }
        
        [Fact]
        public void NotifyProductAdded_CallsObserverUpdate()
        {
            // Arrange
            // var product = new Product { Id = 1, Name = "Test" };

            // Skapar en mock av INotificationObserver
            // var mockObserver = new Mock<INotificationObserver>();

            // Skapar en instans av ProductSubject och l�gger till mock-observat�ren
            var productSubject = new ProductSubject();
            // productSubject.Attach(mockObserver.Object);

            // Injicerar v�rt eget ProductSubject i UnitOfWork
            // var unitOfWork = new UnitOfWork(productSubject);

            // Act
            // unitOfWork.NotifyProductAdded(product);

            // Assert
            // Verifierar att Update-metoden kallades p� v�r mock-observat�r
            // mockObserver.Verify(o => o.Update(product), Times.Once);
        }
    }
}
