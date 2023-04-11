using Domain;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserService
{
    public class UserService : IUserService
    {
        private readonly OrderManageDbContext _context;
        public UserService(OrderManageDbContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetAll()
        {
            var users = _context.Users.AsEnumerable();
            return users;
        }

        public User GetById(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            return user;
        }
    }
}
