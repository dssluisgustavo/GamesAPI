using Domain;
using Npgsql;
using NPOI.SS.Formula.Functions;
using Repository.Interfaces;
using System.Collections;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        public const string connectionString = ("User Id=postgres;Password=C2WKIjQEdr4BsF5d;Server=db.blditmfikaiulhyinehk.supabase.co;Port=5432;Database=postgres");

        public User GetById(int id)
        {

            using (NpgsqlDataSource data = NpgsqlDataSource.Create(connectionString))
            {
                using (NpgsqlCommand command = data.CreateCommand($"SELECT id FROM \"user\" WHERE id = {id}"))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        User user = new User();

                        user.Username = reader.GetString(1);
                        user.Password = reader.GetString(2);
                        user.Email = reader.GetString(3);

                        return user;
                    }
                }
            }
        }

        public User GetByUsername(string username)
        {

            using (NpgsqlDataSource data = NpgsqlDataSource.Create(connectionString))
            {
                using (NpgsqlCommand command = data.CreateCommand($"SELECT id, username, password, email FROM \"user\" WHERE username = '{username}'"))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        bool hasline = reader.Read();

                        if (hasline == false)
                        {
                            return null;
                        }

                        User user = new User();

                        user.Username = reader.GetString(1);
                        user.Password = reader.GetString(2);
                        user.Email = reader.GetString(3);

                        //tratar fluxo de usuário

                        return user;
                    }
                }
            }
        }

        public int CreateUser(User user)
        {
            using (NpgsqlDataSource data = NpgsqlDataSource.Create(connectionString))
            {
                using (NpgsqlCommand command = data.CreateCommand($"INSERT INTO \"user\" (username, \"password\", email) VALUES ('{user.Username}', '{user.Password}', '{user.Email}') RETURNING id"))
                {
                    int newUserId = (int)command.ExecuteScalar();

                    return newUserId;
                }
            }
        }

        public string RecoverPassword(string password)
        {
            using (NpgsqlDataSource data = NpgsqlDataSource.Create(connectionString))
            {
                using (NpgsqlCommand command = data.CreateCommand($"UPDATE \"user\" SET \"password\" = {password}"))
                {
                    command.ExecuteNonQuery();

                    return password;
                }
            }
        }
    }
}