// See https://aka.ms/new-console-template for more information
using Domain;
using GamesAPI;
using GamesAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

Console.WriteLine("Digete:");


var service = new LoginService();
var user = new User();

user.username = Console.ReadLine();
user.password = Console.ReadLine();

var result = service.CreateUser(user);
Console.WriteLine(result);



/*string connectionstring = ("User Id=postgres;Password=C2WKIjQEdr4BsF5d;Server=db.blditmfikaiulhyinehk.supabase.co;Port=5432;Database=postgres");

//NpgsqlDataSource datasource = NpgsqlDataSource.Create(connectionstring);
using (NpgsqlDataSource datasource = NpgsqlDataSource.Create(connectionstring))
{
    using (NpgsqlCommand command = datasource.CreateCommand("SELECT * FROM platform"))
    {
        using (NpgsqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(1));
            }
        } 

    }

} */

//NpgsqlCommand command = datasource.CreateCommand("SELECT * FROM platform");

//NpgsqlDataReader reader = command.ExecuteReader();

//while (reader.Read())
//{
//    Console.WriteLine(reader.GetString(1));
//}


