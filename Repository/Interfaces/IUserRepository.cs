﻿using Domain;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        ValidUser GetById(int id);

        ValidUser GetByUsername(string username);

        int CreateUser(ValidUser user);

        string RecoverPassword (string password);
    }
}
