using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }

    }
}
