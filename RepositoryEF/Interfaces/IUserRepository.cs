using Domain;
using RepositoryEF.Models;
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

        void CreateRefreshToken(UserToken user);

        User ReturnToken(string username);

        int CreateUser(User user);

        void Logout(string user);

        void SaveChanges();
        UserToken GetTokenByUserId(int id);
    }
}
