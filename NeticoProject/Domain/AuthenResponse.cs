using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public  class AuthenResponse
    {
        public User? User { get; set; }
        public string? Token { get; set; }
    }
}
