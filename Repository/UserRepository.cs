using Domain;
using Npgsql;
using System.Collections;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net;

namespace Repository
{
    public class UserRepository
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

                        user.username = reader.GetString(1);
                        user.password = reader.GetString(2);
                        user.email = reader.GetString(3);

                        return user;
                    }
                }
            }
        }

        public User GetByUserName(string username)
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

                        user.username = reader.GetString(1);
                        user.password = reader.GetString(2);
                        user.email = reader.GetString(3);

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
                using (NpgsqlCommand command = data.CreateCommand($"INSERT INTO \"user\" (username, \"password\", email) VALUES ('{user.username}', '{user.password}', '{user.email}') RETURNING id"))
                {
                    int id = (int)command.ExecuteScalar();

                    return id;
                }
            }
        }

    }
}