using Domain;
using Microsoft.EntityFrameworkCore.Storage;
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
            if (createUser.Username.Length.IsBetween(4, 10) && createUser.Password.Length.IsBetween(8, 16))
            {
                Regex validateEmailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                Match match = validateEmailRegex.Match(createUser.Email);

                if (match.Success)
                {
                    User user = new User();

                    user.Id = createUser.Id;
                    user.Username = createUser.Username;
                    user.Email = createUser.Email;
                    user.Salt = CreateSalt(8);
                    user.Password = Crypto.GenerateMD5(user.Salt + user.Password);

                    user.UserToken = new UserToken() { User = user, RefreshToken = "", ExpirationDate = DateTime.Now };

                    int newUser = userRepository.CreateUser(user);

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

        private string CreateSalt(int numberOfLetters)
        {
            string salt = "";
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            Random random = new Random();

            for (int i = 0; i < numberOfLetters; i++)
            {
                int index = random.Next(0, letters.Length);
                string indexLetter = letters[index].ToString();

                salt += indexLetter;

            }

            return salt;
        }
    }
}
