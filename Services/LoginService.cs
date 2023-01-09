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
            var user = repository.GetByUserName(login.username);

            if (user != null && user.password == login.password)
            {
                return true;
            }

            return false;
        }

      
    }


}
