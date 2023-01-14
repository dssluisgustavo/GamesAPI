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
    public class PublisherRepository
    {
        public const string connectionString = ("User Id=postgres;Password=C2WKIjQEdr4BsF5d;Server=db.blditmfikaiulhyinehk.supabase.co;Port=5432;Database=postgres");


        public List<PublisherSales> GetAll()
        {
            var publisherList = new List<PublisherSales>();

            using (NpgsqlDataSource data = NpgsqlDataSource.Create(connectionString))
            {
                using (NpgsqlCommand command = data.CreateCommand(@"
                       SELECT publisher.id, publisher.publisher_name, SUM(num_sales)
                       FROM publisher
                       JOIN game_publisher ON publisher.id = game_publisher.publisher_id
                       JOIN game_platform ON game_publisher.publisher_id = game_platform.game_publisher_id 
                       JOIN region_sales ON game_publisher.publisher_id = region_sales.game_platform_id 
                       GROUP BY publisher.id 
                       ORDER BY id"))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PublisherSales publisher = new PublisherSales();
                            publisher.publisher = new Publisher();

                            publisher.publisher.Id = reader.GetInt32(0);
                            publisher.publisher.Name = reader.GetString(1);
                            publisher.Sales = reader.GetDouble(2);

                            publisherList.Add(publisher);
                        }

                        return publisherList;
                    }
                }
            }
        }

        public PublisherDetails GetById(int id)
        {
            PublisherDetails getDetails = new PublisherDetails();

            using (NpgsqlDataSource data = NpgsqlDataSource.Create(connectionString))
            {
                using (NpgsqlCommand command = data.CreateCommand($@"SELECT * FROM publisher WHERE publisher.id = {id}"))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        getDetails.publisher = new Publisher();

                        getDetails.publisher.Id = reader.GetInt32(0);
                        getDetails.publisher.Name = reader.GetString(1);

                    }
                    using (NpgsqlCommand newCommand = data.CreateCommand($@"
                                SELECT p.id, p.platform_name, SUM (rs.num_sales) 
                                FROM platform p 
                                JOIN game_platform gp ON p.id = gp.platform_id 
                                JOIN game_publisher gp2 ON gp.game_publisher_id = gp2.publisher_id 
                                JOIN region_sales rs ON gp.platform_id = rs.game_platform_id 
                                WHERE gp.game_publisher_id = {id}
                                GROUP BY p.id, p.platform_name 
                                ORDER BY p.id"))
                    {
                        using (NpgsqlDataReader newReader = newCommand.ExecuteReader())
                        {
                            getDetails.platformList = new List<PlatformSales>();
                            

                            while (newReader.Read())
                            {
                                PlatformSales platformInfos = new PlatformSales();
                                platformInfos.platform = new Platform();

                                platformInfos.platform.Id = newReader.GetInt32(0);
                                platformInfos.platform.Name = newReader.GetString(1);
                                platformInfos.Sales = newReader.GetDouble(2);

                                getDetails.platformList.Add(platformInfos);
                            }

                            return getDetails;
                        }
                    }
                }
            }
        }
    }
}
