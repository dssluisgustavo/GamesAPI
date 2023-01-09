using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService
    {
        public UserRepository repository = new UserRepository();
        public int CreateUser(User newuser)
        {
            var idNewUser = repository.CreateUser(newuser);

            return idNewUser;
        }


        public User CreateNewPassword(string username)
        {
            var getUser = repository.GetByUserName(username);

            return getUser;
        }

    }
}
