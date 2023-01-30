using Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository.Interfaces;
using RepositoryEF.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RefreshTokenProvider : IRefreshTokenProvider
    {
        IUserRepository _userRepository;
        public RefreshTokenProvider(IUserRepository repository)
        {
            _userRepository = repository;
        }
        public string NewToken(User user)
        {
            string email = user.Email;
            string salt = user.Salt;

            DateTime lifeTime = DateTime.Now;
            lifeTime = lifeTime.AddHours(4);

            string infosToMd5 = email + salt + lifeTime;

            string refreshToken = Crypto.GenerateMD5(infosToMd5);

            UserToken userToken = _userRepository.GetTokenByUserId(user.Id);

            if (userToken == null)
            {
                userToken = new UserToken()
                {
                    RefreshToken = refreshToken,
                    ExpirationDate = lifeTime,
                    User = user
                };
                _userRepository.CreateRefreshToken(userToken);
            }

            userToken.RefreshToken = refreshToken;
            userToken.ExpirationDate = lifeTime;
            userToken.User = user;

            _userRepository.SaveChanges();

            return refreshToken;
        }
    }
}
