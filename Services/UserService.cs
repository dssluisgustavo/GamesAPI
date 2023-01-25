using Domain;
using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using Repository;
using Repository.Interfaces;
using RepositoryEF.Models;
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

        public int CreateUser(ValidUser createUser)
        {
            User user = new User();

            user.Id = createUser.Id;
            user.Username= createUser.Username;
            user.Password= createUser.Password;
            user.Email= createUser.Email;

            int newUser = userRepository.CreateUser(user);

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

        public ValidUser ForgotPassword(string username)
        {
            User user = userRepository.GetByUsername(username);
            
            if (user == null)
            {
                return null;
            }
 
            return null;
        }

        /*public User RecoverPassword (string username)
        {
            var getPassword = repository.RecoverPassword(User.password);

            return getPassword;
        }
        */
    }
}
