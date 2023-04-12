using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using Service.OrderService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Service.OrderService.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        
        [TestMethod()]
        public void GetAll_Return_True()
        {
            var options = new DbContextOptionsBuilder<OrderManageDbContext>()
                        .UseInMemoryDatabase(databaseName: "ORDER_MANAGE").Options;
            using (var context = new OrderManageDbContext(options))
            {
                var category = new Category();
                category.Id = 1;
                category.Name = "Phone";
                category.Description = "hêhe";
                context.Categories.Add(category);

                var customer = new Customer();
                customer.Id = 1;
                customer.Name = "Long";
                customer.Address = "Ha Noi";
                context.Customers.Add(customer);

                var product = new Product();
                product.Id = 1;
                product.Name = "Iphone";
                product.Quantity = 10;
                product.Price = 10;
                product.CategoryId = 1;
                product.Description = "hehe";
                context.Products.Add(product);

                var orderNew = new Order();
                orderNew.CustomerId = 1;
                orderNew.ProductId = 1;
                orderNew.Amount = 2;
                orderNew.OrderDate = DateTime.Now;
               
                context.Orders.Add(orderNew);
                context.SaveChanges();
            
           
                OrderService orderService = new OrderService(context, null, null);
                var pageIndex = 1;
                var pageSize = 10;
                var order = orderService.GetPagging(pageIndex, pageSize);
                Assert.IsNotNull(order);
            }
        }
    }
}