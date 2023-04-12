using Service.OrderService;

namespace TestService
{
    public class Tests
    {
        private IOrderService _orderService;
        public Tests(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAllOrder_Return_True()
        {
            
        }
    }
}