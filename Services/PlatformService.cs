using Domain;
using NPOI.SS.Formula.Functions;
using Repository;
using Repository.Interfaces;
using RepositoryEF.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [Serializable]
    public class PlatformService : IPlatformService
    {
        private readonly IPlatformRepository platformRepository;
        public PlatformService(IPlatformRepository platformRepositoryInterface)
        {
            platformRepository = platformRepositoryInterface;
        }

        public List<PlatformSales> GetAll()
        {
            List<Platform> platformList = platformRepository.GetAll();
            List<PlatformSales> platformSalesList = new List<PlatformSales>();

            foreach (Platform platform in platformList)
            {
                PlatformSales PlatformConversion = new PlatformSales();

                PlatformConversion.PlatformId = platform.Id;
                PlatformConversion.PlatformName = platform.PlatformName;
                PlatformConversion.PlatformUrl = ConfigURL.base_url + $"platforms/{platform.Id}";

                decimal sales = 0;

                foreach (GamePlatform gp in platform.GamePlatforms)
                {
                    foreach (RegionSale regionSale in gp.RegionSales)
                    {
                        sales += regionSale.NumSales.Value;
                    }
                }
                PlatformConversion.Sales = sales;

                platformSalesList.Add(PlatformConversion);

            }
            return platformSalesList;

        }

        public PlatformDetails GetById(int id)
        {
            Platform platform = platformRepository.GetById(id);

            PlatformDetails platformDetails = new PlatformDetails();

            platformDetails.Publishers = new List<PublisherSales>();
            platformDetails.Games = new List<GameSales>();


            platformDetails.PlatformId = platform.Id;
            platformDetails.PlatformName = platform.PlatformName;
            platformDetails.PlatformUrl = ConfigURL.base_url + $"platform/{platform.Id}";

            decimal sales = 0;

            foreach (GamePlatform gp in platform.GamePlatforms)
            {
                foreach (RegionSale rs in gp.RegionSales)
                {
                    sales += rs.NumSales.Value;
                }
            }
            platformDetails.Sales = sales;

            foreach (GamePlatform gp in platform.GamePlatforms)
            {
                if(!platformDetails.Publishers.Any(p => p.PublisherId == gp.GamePublishers.Publisher.Id))
                {
                    PublisherSales publishersElement = new PublisherSales();

                    publishersElement.PublisherId = gp.GamePublishers.Publisher.Id;
                    publishersElement.PublisherName = gp.GamePublishers.Publisher.PublisherName;
                    publishersElement.PublisherUrl = ConfigURL.base_url + $"publisher/{gp.GamePublishers.Publisher.Id}";

                    platformDetails.Publishers.Add(publishersElement);

                }

                if (!platformDetails.Games.Any(g => g.GameId == gp.GamePublishers.Game.Id))
                {
                    GameSales gamesElement = new GameSales();

                    gamesElement.GameId = gp.GamePublishers.Game.Id;
                    gamesElement.GameName = gp.GamePublishers.Game.GameName;
                    gamesElement.GameUrl = ConfigURL.base_url + $"game/{gp.GamePublishers.Game.Id}";

                    platformDetails.Games.Add(gamesElement);
                }
            }

            if (platformDetails == null)
            {
                return null;
            }

            return platformDetails;
        }
    }
}
