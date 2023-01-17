using Domain;
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
        T GetById(int id);

        T GetByUseraname(string username);

        T CreateUser(User user);

        T RecoverPassword (string password);
    }
}
