using Domain;
using Npgsql;
using NPOI.SS.Formula.Functions;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GameRepository : IGameRepository
    {
        public const string connectionString = ("User Id=postgres;Password=C2WKIjQEdr4BsF5d;Server=db.blditmfikaiulhyinehk.supabase.co;Port=5432;Database=postgres");

        public List<GameSales> GetAll()
        {
            var gamesList = new List<GameSales>();

            using (NpgsqlDataSource data = NpgsqlDataSource.Create(connectionString))
            {
                using (NpgsqlCommand command = data.CreateCommand($@"
                            SELECT game.id, game.game_name, genre.genre_name 
                            FROM game
                            JOIN genre ON game.genre_id = genre.id 
                            ORDER BY game.id"))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GameSales game = new GameSales();
                            game.Game = new Game();

                            game.Game.Id = reader.GetInt32(0);
                            game.Game.Name = reader.GetString(1);
                            game.Game.Genre = reader.GetString(2);

                            gamesList.Add(game);
                        }

                        return gamesList;
                    }
                }
            }
        }

        public GameDetails GetById(int id)
        {
            GameDetails gameDetails = new GameDetails();

            using (NpgsqlDataSource data = NpgsqlDataSource.Create(connectionString))
            {
                using (NpgsqlCommand command = data.CreateCommand($@"
                            SELECT game.id, game.game_name, genre.genre_name 
                            FROM game
                            JOIN genre ON game.genre_id = genre.id 
                            WHERE game.id = {id}
                            ORDER BY game.id"))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        bool hasLine = reader.Read();

                        if (hasLine == false)
                        {
                            return null ;
                        }

                        gameDetails.Game = new Game();

                        gameDetails.Game.Id = reader.GetInt32(0);
                        gameDetails.Game.Name = reader.GetString(1);
                        gameDetails.Game.Genre = reader.GetString(2);
                    }
                }
                using (NpgsqlCommand newCommand = data.CreateCommand($@"
                            SELECT p.id, p.publisher_name, SUM(rs.num_sales)
                            FROM game g 
                            JOIN game_publisher gp ON g.id = gp.game_id 
                            JOIN publisher p ON gp.game_id = p.id 
                            JOIN game_platform gp2 ON p.id = gp2.platform_id 
                            JOIN region_sales rs ON gp2.platform_id = rs.game_platform_id
                            WHERE p.id = {id}
                            GROUP BY p.id 
                            ORDER BY p.id"))
                {
                    using (NpgsqlDataReader newReader = newCommand.ExecuteReader())
                    {
                        gameDetails.Publisher = new List<PublisherSales>();

                        while (newReader.Read())
                        {
                            PublisherSales publisherDetails = new PublisherSales();
                            publisherDetails.Publisher = new Publisher();

                            publisherDetails.Publisher.Id = newReader.GetInt32(0);
                            publisherDetails.Publisher.Name = newReader.GetString(1);
                            publisherDetails.Sales = newReader.GetDouble(2);

                            gameDetails.Publisher.Add(publisherDetails);
                        }
                    }
                }
                using (NpgsqlCommand commandNew = data.CreateCommand($@"
                                SELECT p.id, p.platform_name, SUM (r.num_sales)
                                FROM game g 
                                JOIN game_publisher gp ON g.id = gp.game_id
                                JOIN game_platform gp2 ON gp.game_id = gp2.platform_id 
                                JOIN platform p ON gp2.platform_id = p.id 
                                JOIN region_sales r ON gp2.platform_id = r.game_platform_id 
                                WHERE g.id = {id}
                                GROUP BY p.id 
                                ORDER BY p.id"))
                {
                    using (NpgsqlDataReader readerNew = commandNew.ExecuteReader())
                    {
                        gameDetails.Platform = new List<PlatformSales>();

                        while (readerNew.Read())
                        {
                            PlatformSales platformDetails = new PlatformSales();
                            platformDetails.Platform = new Platform();

                            platformDetails.Platform.Id= readerNew.GetInt32(0);
                            platformDetails.Platform.Name = readerNew.GetString(1);
                            platformDetails.Sales = readerNew.GetDouble(2);

                            gameDetails.Platform.Add(platformDetails);
                        }
                    }
                }
                using (NpgsqlCommand otherCommad = data.CreateCommand($@"
                                SELECT r.id, r.region_name, SUM(rs.num_sales)
                                FROM game g 
                                JOIN game_publisher gp ON g.id = gp.game_id 
                                JOIN game_platform gp2 ON gp.game_id = gp2.platform_id 
                                JOIN region_sales rs ON gp2.platform_id = rs.game_platform_id 
                                JOIN region r ON rs.game_platform_id = r.id 
                                WHERE g.id = {id}
                                GROUP BY r.id
                                ORDER BY r.id"))
                {
                    using (NpgsqlDataReader otherReader = otherCommad.ExecuteReader())
                    {
                        gameDetails.Region = new List<Region_Sales>();

                        while (otherReader.Read())
                        {
                            Region_Sales regionDetails = new Region_Sales();
                            regionDetails.Region = new Region();

                            regionDetails.Region.Id = otherReader.GetInt32(0);
                            regionDetails.Region.Name = otherReader.GetString(1);
                            regionDetails.SalesValue= otherReader.GetDouble(2);

                            gameDetails.Region.Add(regionDetails);
                        }

                        return gameDetails;
                    }
                }
            }
        }

    }
}
