using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class SearchModel
    {
        [Required]
        public int PageIndex { get; set; }
        [Required]
        public int PageSize { get; set; }
        public string? SearchText { get; set; }

    }
}
