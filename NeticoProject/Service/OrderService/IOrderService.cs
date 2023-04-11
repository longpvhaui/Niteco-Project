using Domain;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.OrderService
{
    public interface IOrderService
    {
        OrderResponse GetPagging(int pageIndex, int pageSize);
        IEnumerable<Order> GetAll();
        Order GetOrder(int id);
        ServiceResponse<Order> CreateOrder(OrderManage order);
        OrderResponse GetOrderSearch(SearchModel model);
    }
}
