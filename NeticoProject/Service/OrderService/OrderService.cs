using Azure;
using Domain;
using Domain.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly OrderManageDbContext _context;
        private readonly ProductService _productService;
        private readonly ILogger<OrderService> _logger;
        public OrderService(OrderManageDbContext context, ProductService productService,ILogger<OrderService> logger)
        {
            _context = context;
            _productService = productService;
            _logger = logger;
        }
        public ServiceResponse<Order> CreateOrder(OrderManage order)
        {
            var response = new ServiceResponse<Order>();
            try
            {
                
                var orders = GetAll();
                var products = _productService.GetAll();
                var orderExist = orders.FirstOrDefault(x => x.Id == order.Id);
                var product = products.FirstOrDefault(x => x.Id == order.ProductId);
                if (orderExist != null)
                {
                    response.Success = false;
                    response.Message = "Order already  exits";
                }
                if (product != null && product.Quantity < order.Amount)
                {
                    response.Success = false;
                    response.Message = "There are not enough products";
                }
                else
                {

                    var orderNew = new Order();
                    orderNew.CustomerId = order.CustomerId;
                    orderNew.ProductId = order.ProductId;
                    orderNew.Amount = order.Amount;
                    orderNew.OrderDate = DateTime.Now;
                    _context.Orders.Add(orderNew);
                    _context.SaveChanges();
                    response.Success = true;
                    response.Message = "Create order success";
                    response.Data = orderNew;
                }
                return response;
            }catch(Exception ex)
            {
                response.Success=false;
                response.Message = ex.Message;
                _logger.LogError(ex.Message);
                return response;
            }

        }
        public IEnumerable<Order> GetAll()
        {
            var orders = _context.Orders
                        .Include(o => o.Customer)
                        .Include(o => o.Product.Category).AsEnumerable();
            return orders;
        }

        public Order GetOrder(int id)
        {
            throw new NotImplementedException();
        }

        public OrderResponse GetOrderSearch(SearchModel model)
        {
            var response = new OrderResponse();
            if (model.PageSize <= 0 || model.PageIndex <= 0)
            {
                return null;
            }
            var orders = GetAll();

            if (!string.IsNullOrEmpty(model.SearchText))
            {
                var searchText = model.SearchText.ToLower();
                orders = orders.Where(x => x.Product.Name.ToLower().Contains(searchText) || x.Customer.Name.Contains(searchText) || x.Product.Category.Name.Contains(searchText));
            }



            var orderPagging = orders.Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize).ToList();
            var totalItems = orders.ToList().Count;
            var totalPages = Math.Ceiling((double)totalItems / model.PageSize);
            response.Orders = orderPagging;
            response.TotalItems = totalItems;
            response.TotalPages = totalPages;

            return response;
        }

        public OrderResponse GetPagging(int pageIndex, int pageSize)
        {
            if (pageIndex <= 0 || pageSize <= 0)
            {
                return null;
            }
            var response = new OrderResponse();
            var orderAll = GetAll();
            var orders = orderAll.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var totalItems = orderAll.ToList().Count;
            var totalPages = Math.Ceiling((double)totalItems / pageSize);
            response.Orders = orders;
            response.TotalItems = totalItems;
            response.TotalPages = totalPages;

            return response;
        }

    }
}
