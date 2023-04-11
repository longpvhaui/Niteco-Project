using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class OrderResponse
    {
        public List<Order> Orders { get; set; }
        public int TotalItems { get; set; }
        public double TotalPages { get; set; }
    }
}
