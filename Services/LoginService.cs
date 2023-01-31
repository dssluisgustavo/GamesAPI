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
        private readonly IRefreshTokenProvider providerRT;

        public LoginService(IUserRepository userRepositoryInterface, IJwtProvider providerJwt, IRefreshTokenProvider providerRefreshToken)
        {
            userRepository = userRepositoryInterface;
            providerJWT = providerJwt;
            providerRT = providerRefreshToken;
        }

        public LoginData Login(Login login)
        {
            User user = userRepository.GetByUsername(login.Username);

            string userPassword = Crypto.GenerateMD5(user.Salt + user.Password);

            if (user == null || user.Password != userPassword)
            {
                return null;
            }

            LoginData tokenData = new LoginData();

            tokenData. jwtToken = providerJWT.NewToken(user);
            tokenData.refreshToken = providerRT.NewToken(user);


            return tokenData;
        }

        public LoginData LoginWithRefresh(LoginWithRefresh refreshToken)
        {
            User user = userRepository.ReturnToken(refreshToken.Username);

            if (user != null)
            {
                if (user.UserToken.RefreshToken == refreshToken.RefreshToken)
                {
                    if (user.UserToken.ExpirationDate >= DateTime.Now)
                    {
                        LoginData saveToken = new LoginData();

                        string newToken = providerJWT.NewToken(user);
                        user.UserToken.ExpirationDate = DateTime.Now.AddDays(4);

                        saveToken.jwtToken = newToken;
                        saveToken.refreshToken = user.UserToken.RefreshToken;

                        userRepository.SaveChanges();
                        return saveToken;
                    }
                }
            }
            return null;
        }
        public void Logout(string username)
        {
            userRepository.Logout(username);
        }
    }


}
