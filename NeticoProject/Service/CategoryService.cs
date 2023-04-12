using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService
    {
        private readonly OrderManageDbContext _context;
        public CategoryService(OrderManageDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetAll()
        {
            var categories = _context.Categories.AsEnumerable();
            return categories;
        }
        public Category GetById(int id)
        {
            var category = _context.Categories.SingleOrDefault(u => u.Id == id);
            return category;
        }
    }
}
