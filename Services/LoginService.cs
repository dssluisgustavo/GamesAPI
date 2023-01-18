using Domain;
using Repository;
using Repository.Interfaces;
using Services.Interfaces;

namespace GamesAPI
{
    [Serializable]
    public class LoginService : ILoginService
    {
        private readonly IUserRepository userRepository;
        public LoginService(IUserRepository userRepositoryInterface)
        {
            userRepository = userRepositoryInterface;
        }

        public bool Login(Login user)
        {
            User login = userRepository.GetByUsername(user.Username);

            if (login != null && login.Password == user.Password)
            {
                return true;
            }

            return false;
        }


    }


}
