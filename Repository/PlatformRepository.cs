using Domain;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PlatformRepository
    {
        public const string connectionString = ("User Id=postgres;Password=C2WKIjQEdr4BsF5d;Server=db.blditmfikaiulhyinehk.supabase.co;Port=5432;Database=postgres");

        public List<PlatformSales> GetAll()
        {
            var platformList = new List<PlatformSales>();

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
                            PlatformSales platformInfo = new PlatformSales();
                            platformInfo.platform = new Platform();

                            platformInfo.platform.Id = reader.GetInt32(0);
                            platformInfo.platform.Name = reader.GetString(1);
                            platformInfo.Sales = reader.GetDouble(2);

                            platformList.Add(platformInfo);
                        }

                        return platformList;
                    }
                }
            }
        }

        public PlatformDetails GetById(int id)
        {
            PlatformDetails platform = new PlatformDetails();

            using (NpgsqlDataSource data = NpgsqlDataSource.Create(connectionString))
            {
                using (NpgsqlCommand command = data.CreateCommand(@$"SELECT * FROM platform WHERE id = {id}"))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        platform.platform = new Platform();

                        platform.platform.Id = reader.GetInt32(0);
                        platform.platform.Name = reader.GetString(1);

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
                            platform.publishers = new List<PublisherSales>();

                            while (newReader.Read())
                            {
                                PublisherSales publisherInfo = new PublisherSales();
                                publisherInfo.publisher = new Publisher();

                                publisherInfo.publisher.Id = newReader.GetInt32(0);
                                publisherInfo.publisher.Name = newReader.GetString(1);

                                platform.publishers.Add(publisherInfo);
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
                                platform.games = new List<GameSales>();

                                while (readerNew.Read())
                                {
                                    GameSales gamesInfo = new GameSales();
                                    gamesInfo.game = new Game();

                                    gamesInfo.game.Id = readerNew.GetInt32(0);
                                    gamesInfo.game.Name = readerNew.GetString(1);

                                    platform.games.Add(gamesInfo);
                                }

                                return platform;
                            }
                        }

                    }
                }
            }
        }
    }
}
