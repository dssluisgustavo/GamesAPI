using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [Serializable]
    public class UserService
    {
        public UserRepository repository = new UserRepository();
        public int CreateUser(User newuser)
        {
            var idNewUser = repository.CreateUser(newuser);

            return idNewUser;
        }


        public User ForgotPassword(string username)
        {
            var getUser = repository.GetByUserName(username);

            return getUser;
        }

        /*public User RecoverPassword (string username)
        {
            var getPassword = repository.RecoverPassword(User.password);

            return getPassword;
        }
        */
    }
}
