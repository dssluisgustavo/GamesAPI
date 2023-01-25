using Domain;
using NPOI.SS.Formula.Functions;
using Repository;
using Repository.Interfaces;
using RepositoryEF.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;
        public GameService(IGameRepository gameRepositoryInterface)
        {
            gameRepository = gameRepositoryInterface;
        }

        public List<GameSales> GetAll()
        {
            List<Game> gamesList = gameRepository.GetAll();
            List<GameSales> gamesSalesList = new List<GameSales>();

            foreach (Game game in gamesList)
            {
                GameSales GameConversion = new GameSales();

                GameConversion.GameId = game.Id;
                GameConversion.GameName = game.GameName;
                GameConversion.GameUrl = ConfigURL.base_url + $"games/{game.Id}";

                decimal sales = 0;

                foreach (GamePublisher gp in game.GamePublishers)
                {
                    foreach (GamePlatform gp2 in gp.GamePlatforms)
                    {
                        foreach (RegionSale rs in gp2.RegionSales)
                        {
                            sales += rs.NumSales.Value;
                        }
                    }
                }
                GameConversion.Sales = sales;

                gamesSalesList.Add(GameConversion);

            }
            return gamesSalesList;
        }

        public GameDetails GetById(int id)
        {
            Game game = gameRepository.GetById(id);

            GameDetails gameDetails = new GameDetails();

            gameDetails.Platform = new List<PlatformSales>();
            gameDetails.Publisher = new List<PublisherSales>();
            gameDetails.Region = new List<Region_Sales>();

            gameDetails.GameId = game.Id;
            gameDetails.GameName = game.GameName;
            gameDetails.GameUrl = ConfigURL.base_url + $"game/{game.Id}";

            decimal sales = 0;

            foreach (GamePublisher gp in game.GamePublishers)
            {
                foreach (GamePlatform gp2 in gp.GamePlatforms)
                {
                    foreach (RegionSale rs in gp2.RegionSales)
                    {
                        sales += rs.NumSales.Value;
                    }
                }
            }
            gameDetails.Sales = sales;

            foreach (GamePublisher gp in game.GamePublishers)
            {
                if (!gameDetails.Publisher.Any(g => g.PublisherId == gp.Publisher.Id))
                {
                    PublisherSales publisherElement = new PublisherSales();

                    publisherElement.PublisherId = gp.Publisher.Id;
                    publisherElement.PublisherName = gp.Publisher.PublisherName;
                    publisherElement.PublisherUrl = ConfigURL.base_url + $"publishers/{gp.PublisherId}";

                    decimal publisherSales = 0;

                    foreach (GamePlatform gp2 in gp.GamePlatforms)
                    {
                        foreach (RegionSale publisherNumSales in gp2.RegionSales)
                        {
                            publisherSales += publisherNumSales.NumSales.Value;
                        }
                    }
                    publisherElement.Sales = publisherSales;

                    gameDetails.Publisher.Add(publisherElement);
                }

                foreach (GamePlatform gp3 in gp.GamePlatforms)
                {
                    if (!gameDetails.Platform.Any(g => g.PlatformId == gp3.Platform.Id))
                    {
                        PlatformSales platformElement = new PlatformSales();

                        platformElement.PlatformId = gp3.Platform.Id;
                        platformElement.PlatformName = gp3.Platform.PlatformName;
                        platformElement.PlatformUrl = ConfigURL.base_url + $"platforms/{gp3.Platform.Id}";

                        decimal platformSales = 0;

                        foreach (RegionSale platformNumSales in gp3.RegionSales)
                        {
                            platformSales += platformNumSales.NumSales.Value;
                        }
                        platformElement.Sales = platformSales;

                        gameDetails.Platform.Add(platformElement);  
                    }

                    foreach (RegionSale rs in gp3.RegionSales)
                    {
                        Region_Sales regionSalesElement = new Region_Sales();

                        regionSalesElement.RegionId = rs.Region.Id;
                        regionSalesElement.RegionName = rs.Region.RegionName;

                        decimal regionSales = 0;

                        regionSalesElement.SalesValue = regionSales += rs.NumSales.Value;

                        gameDetails.Region.Add(regionSalesElement);
                    }
                }
            }

            if (gameDetails == null)
            {
                return null;
            }
            return gameDetails;
        }
    }
}
