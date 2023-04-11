using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Encrypt
{
    public class MD5Encrypt : IMD5Encrypt
    {
        public string EncryptPassword(string password)
        {
            //encoded đoạn password input
            MD5 md5 = new MD5CryptoServiceProvider();
            var originalBytes = Encoding.Default.GetBytes(password);
            var encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes).Replace("-", "");
        }
    }
}
