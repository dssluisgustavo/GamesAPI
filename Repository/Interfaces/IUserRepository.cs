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
        User GetById(int id);

        User GetByUsername(string username);

        int CreateUser(User user);

        string RecoverPassword (string password);
    }
}
