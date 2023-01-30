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
using System.Security.Cryptography;
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
            user.Username = createUser.Username;
            user.Password = createUser.Password;
            user.Email = createUser.Email;

            user.Password = Crypto.GenerateMD5(user.Password);

            string salt = "";
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZÇ";
            string numbers = "123456789";

            for (int i = 0; i < 8; i++)
            {
                string indexLetter = letters[i].ToString();
                string indexNum = numbers[i].ToString();

                string sum = indexLetter + indexNum.ToString();

                salt += sum;

            }

            user.Salt = salt;

            string toCrypto = user.Salt + user.Password;

            string stringCrypto = Crypto.GenerateMD5(toCrypto);

            userRepository.SaveChanges();

            int newUser = userRepository.CreateUser(user);
            // antes de criar o user, criptografar a senha dele
            // cria o Salt
            // soma password + Salt e cryptgrafa a string
            if (createUser.Username.Length.IsBetween(4, 10) && createUser.Password.Length.IsBetween(8, 16))
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
