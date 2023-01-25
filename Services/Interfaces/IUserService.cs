using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        int CreateUser(ValidUser user);

        ValidUser ForgotPassword(string username);
    }
}
