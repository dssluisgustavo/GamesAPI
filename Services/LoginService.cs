using Domain;
using Repository;
using Repository.Interfaces;
using RepositoryEF.Models;
using Services.Interfaces;

namespace GamesAPI
{
    [Serializable]
    public class LoginService : ILoginService
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtProvider providerJWT;

        public LoginService(IUserRepository userRepositoryInterface, IJwtProvider provider)
        {
            userRepository = userRepositoryInterface;
            providerJWT = provider;
        }

        public string Login(Login user)
        {
            User login = userRepository.GetByUsername(user.Username);

            if (login == null || login.Password != user.Password)
            {
                return null;
            }

            string token = providerJWT.NewToken(login);

            return token;
        }


    }


}
