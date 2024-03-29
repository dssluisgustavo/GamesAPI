﻿using Domain;
using RepositoryEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ILoginService
    {
        LoginData Login(Login login);

        void Logout(string username);

        LoginData LoginWithRefresh(LoginWithRefresh refreshToken);
    }
}
