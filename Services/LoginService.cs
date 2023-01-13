using Domain;
using Repository;

namespace GamesAPI
{
    [Serializable]
    public class LoginService
    {
        public UserRepository repository = new UserRepository();

        public bool Login(Login login)
        {
            var user = repository.GetByUserName(login.Username);

            if (user != null && user.Password == login.Password)
            {
                return true;
            }

            return false;
        }

      
    }


}
