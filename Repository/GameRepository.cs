using Domain;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GameRepository
    {
        public const string connectionString = ("User Id=postgres;Password=C2WKIjQEdr4BsF5d;Server=db.blditmfikaiulhyinehk.supabase.co;Port=5432;Database=postgres");

        public List<GameSales> GetAll()
        {
            var gameList = new List<GameSales>();

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
                            game.game = new Game();

                            game.game.Id = reader.GetInt32(0);
                            game.game.Name = reader.GetString(1);
                            game.game.Genre = reader.GetString(2);

                            gameList.Add(game);
                        }

                        return gameList;
                    }
                }
            }
        }

        public GameDetails GetById(int id)
        {
            GameDetails game = new GameDetails();

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
                        reader.Read();
                        game.game = new Game();

                        game.game.Id = reader.GetInt32(0);
                        game.game.Name = reader.GetString(1);
                        game.game.Genre = reader.GetString(2);
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
                        game.publisher = new List<PublisherSales>();

                        while (newReader.Read())
                        {
                            PublisherSales publisherSales = new PublisherSales();
                            publisherSales.publisher = new Publisher();

                            publisherSales.publisher.Id = newReader.GetInt32(0);
                            publisherSales.publisher.Name = newReader.GetString(1);
                            publisherSales.Sales = newReader.GetDouble(2);

                            game.publisher.Add(publisherSales);
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
                        game.platform = new List<PlatformSales>();

                        while (readerNew.Read())
                        {
                            PlatformSales platformSales = new PlatformSales();
                            platformSales.platform = new Platform();

                            platformSales.platform.Id= readerNew.GetInt32(0);
                            platformSales.platform.Name = readerNew.GetString(1);
                            platformSales.Sales = readerNew.GetDouble(2);

                            game.platform.Add(platformSales);
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
                        game.regionSales = new List<Region_Sales>();

                        while (otherReader.Read())
                        {
                            Region_Sales regionSales = new Region_Sales();
                            regionSales.region = new Region();

                            regionSales.region.Id = otherReader.GetInt32(0);
                            regionSales.region.Name = otherReader.GetString(1);
                            regionSales.SalesValue= otherReader.GetDouble(2);

                            game.regionSales.Add(regionSales);
                        }

                        return game;
                    }
                }
            }
        }
    }
}
