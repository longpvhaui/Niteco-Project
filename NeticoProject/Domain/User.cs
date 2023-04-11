using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string LoginName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
