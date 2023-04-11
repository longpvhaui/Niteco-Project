﻿using Domain;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AuthenService
{
    public  interface IAuthenticate
    {
        User CheckLogin(LoginModel loginModel);
    }
}
