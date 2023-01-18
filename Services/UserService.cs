using Domain;
using NPOI.SS.Formula.Functions;
using Repository;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [Serializable]
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepositoryInterface)
        {
            userRepository = userRepositoryInterface;
        }

        public int CreateUser(User createUser)
        {
            int newUser = userRepository.CreateUser(createUser);

            return newUser;
        }


        public User ForgotPassword(string username)
        {
            User userName = userRepository.GetByUsername(username);

            if (userName == null)
            {
                return null;
            }

            return userName;
        }

        /*public User RecoverPassword (string username)
        {
            var getPassword = repository.RecoverPassword(User.password);

            return getPassword;
        }
        */
    }
}
