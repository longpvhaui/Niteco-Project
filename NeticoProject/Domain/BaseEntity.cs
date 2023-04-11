using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BaseEntity
    {

        public DateTime CreatedDate { get; set; }

        public Boolean IsDelete { get; set; } = false;

        public DateTime? DeletedDate { get; set; }
    }
}
