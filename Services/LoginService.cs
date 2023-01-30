﻿using Domain;
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

        public LoginData Login(Login user)
        {
            User login = userRepository.GetByUsername(user.Username);
            // criptografar o user.password e depois rodar o if
            string userPassword = Crypto.GenerateMD5(user.Password);

            if (login == null || login.Password != user.Password)
            {
                return null;
            }

            LoginData tokenData = new LoginData();

            tokenData. jwtToken = providerJWT.NewToken(login);
            tokenData.refreshToken = providerRT.NewToken(login);


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
