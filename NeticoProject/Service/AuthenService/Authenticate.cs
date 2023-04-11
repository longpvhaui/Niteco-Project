using Domain;
using Domain.ViewModel;
using Infrastructure.Encrypt;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.AuthenService
{
    public class Authenticate : IAuthenticate
    {
        private readonly OrderManageDbContext _context;
        private readonly IMD5Encrypt _md5;
        public Authenticate(OrderManageDbContext context, IMD5Encrypt md5)
        {
            _context = context;
            _md5 = md5;
        }
        public User CheckLogin(LoginModel loginModel)
        {
            var users = _context.Users.AsEnumerable();
            var pass = _md5.EncryptPassword(loginModel.Password).ToLower();
            var user = users.Where(x => x.LoginName == loginModel.LoginName && x.Password.ToLower() == pass).FirstOrDefault();
            if (user != null) return user;
            else return null;
        }
    }
}
