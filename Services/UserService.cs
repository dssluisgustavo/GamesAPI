using Domain;
using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using Repository;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Match = System.Text.RegularExpressions.Match;

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

            if (createUser.Username.Length.IsBetween(4,10) && createUser.Password.Length.IsBetween(8, 15))
            {
                Regex validateEmailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                Match match = validateEmailRegex.Match(createUser.Email);

                if (match.Success)
                {
                    return newUser;
                }
            }
            return 0;
            
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
