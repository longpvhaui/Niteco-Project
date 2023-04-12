using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService
    {
        private readonly OrderManageDbContext _context;
        public ProductService(OrderManageDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAll()
        {
            var products = _context.Products.AsEnumerable();
            return products;
        }
        public Product GetById(int id)
        {
            var product = _context.Products.SingleOrDefault(u => u.Id == id);
            return product;
        }
    }
}
