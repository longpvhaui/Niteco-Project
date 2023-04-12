using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CustomerService
    {
        private readonly OrderManageDbContext _context;
        public CustomerService(OrderManageDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Customer> GetAll()
        {
            var customer = _context.Customers.AsEnumerable();
            return customer;
        }
        public Customer GetById(int id)
        {
            var customer = _context.Customers.SingleOrDefault(u => u.Id == id);
            return customer;
        }
    }
}
