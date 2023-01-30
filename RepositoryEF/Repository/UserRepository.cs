using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using RepositoryEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryEF.Repository
{
    public class UserRepository : IUserRepository
    {
        PostgresContext context;
        public UserRepository(PostgresContext _context)
        {
            context = _context;
        }

        public int CreateUser(User user)
        {
            User newUser = new User();

            newUser.Id = user.Id;
            newUser.Username = user.Username;
            newUser.Email = user.Email;
            newUser.Password = user.Password;

            context.Users.Add(user);
            context.SaveChanges();
            return user.Id;
        }

        public User GetById(int id)
        {
            User user = context.Users.FirstOrDefault(u => u.Id == id);

            return user;
        }

        public User GetByUsername(string username)
        {
            User user = context.Users.FirstOrDefault(u => u.Username == username);

            return user;
        }

        public void Logout(string username)
        {
            UserToken refreshToken = context.UserTokens.Include(t => t.User.Username)
                .FirstOrDefault(t => t.RefreshToken == username);


            if (username != refreshToken.User.Username)
            {
                context.UserTokens.Remove(refreshToken);
            }
        }

        public string RecoverPassword(string password)
        {
            throw new NotImplementedException();
        }

        public void CreateRefreshToken(UserToken token)
        {
            context.UserTokens.Add(token);
            context.SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public User ReturnToken (string username)
        {
            User user = context.Users.Include(u => u.UserToken).FirstOrDefault(u => u.Username == username);

            return user;
        }

        public UserToken GetTokenByUserId(int id)
        {
            User user = context.Users.Include(u => u.UserToken).FirstOrDefault(u => u.Id == id);

            return user?.UserToken;
        }
    }
}
