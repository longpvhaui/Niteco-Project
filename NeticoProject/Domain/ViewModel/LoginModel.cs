﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class LoginModel
    {
        [Required]
        public string? LoginName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
