using Domain;
using Npgsql;
using NPOI.SS.Formula.Functions;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PlatformRepository : IPlatformRepository
    {
        public const string connectionString = ("User Id=postgres;Password=C2WKIjQEdr4BsF5d;Server=db.blditmfikaiulhyinehk.supabase.co;Port=5432;Database=postgres");

        public List<PlatformSales> GetAll()
        {
            var platformsList = new List<PlatformSales>();

            using (NpgsqlDataSource data = NpgsqlDataSource.Create(connectionString))
            {
                using (NpgsqlCommand command = data.CreateCommand(@"
                        SELECT platform.id, platform.platform_name, SUM (num_sales)  
                        FROM platform 
                        JOIN game_platform ON platform.id = game_platform.platform_id 
                        JOIN region_sales ON game_platform.id = region_sales.game_platform_id
                        GROUP BY platform.id 
                        ORDER BY id "))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PlatformSales platform = new PlatformSales();
                            platform.Platform = new Platform();

                            platform.Platform.Id = reader.GetInt32(0);
                            platform.Platform.Name = reader.GetString(1);
                            platform.Sales = reader.GetDouble(2);

                            platformsList.Add(platform);
                        }

                        return platformsList;
                    }
                }
            }
        }

        public PlatformDetails GetById(int id)
        {
            PlatformDetails platformDetails = new PlatformDetails();

            using (NpgsqlDataSource data = NpgsqlDataSource.Create(connectionString))
            {
                using (NpgsqlCommand command = data.CreateCommand(@$"SELECT * FROM platform WHERE id = {id}"))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        bool hasLine = reader.Read();

                        if (hasLine == false)
                        {
                            return null;
                        }

                        platformDetails.Platform = new Platform();

                        platformDetails.Platform.Id = reader.GetInt32(0);
                        platformDetails.Platform.Name = reader.GetString(1);

                    }
                    using (NpgsqlCommand newCommand = data.CreateCommand(@$"
                                    SELECT publisher.id, publisher.publisher_name 
                                    FROM platform
                                    JOIN game_platform ON platform.id = game_platform.platform_id 
                                    JOIN game_publisher ON game_platform.platform_id = game_publisher.publisher_id 
                                    JOIN publisher ON game_publisher.publisher_id = publisher.id 
                                    WHERE platform.id = {id}
                                    GROUP BY publisher.id
                                    ORDER BY publisher.id"))
                    {
                        using (NpgsqlDataReader newReader = newCommand.ExecuteReader())
                        {
                            platformDetails.Publishers = new List<PublisherSales>();

                            while (newReader.Read())
                            {
                                PublisherSales publisherDetails = new PublisherSales();
                                publisherDetails.Publisher = new Publisher();

                                publisherDetails.Publisher.Id = newReader.GetInt32(0);
                                publisherDetails.Publisher.Name = newReader.GetString(1);

                                platformDetails.Publishers.Add(publisherDetails);
                            }
                        }
                        using (NpgsqlCommand commandNew = data.CreateCommand(@$"
                                SELECT game.id, game.game_name 
                                FROM game_platform 
                                JOIN game_publisher ON game_platform.game_publisher_id = game_publisher.game_id  
                                JOIN game ON game_publisher.game_id  = game.id 
                                WHERE game_publisher_id = {id}
                                GROUP BY game.id 
                                ORDER BY game.id"))
                        {
                            using (NpgsqlDataReader readerNew = commandNew.ExecuteReader())
                            {
                                platformDetails.Games = new List<GameSales>();

                                while (readerNew.Read())
                                {
                                    GameSales gameDetails = new GameSales();
                                    gameDetails.Game = new Game();

                                    gameDetails.Game.Id = readerNew.GetInt32(0);
                                    gameDetails.Game.Name = readerNew.GetString(1);

                                    platformDetails.Games.Add(gameDetails);
                                }

                                return platformDetails;
                            }
                        }

                    }
                }
            }
        }
    }
}
