using Domain;
using NPOI.SS.Formula.Functions;
using Repository;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
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
            List<PlatformSales> platformsList = platformRepository.GetAll();

            for (int i = 0; i < platformsList.Count; i++)
            {
                //locallhost/006/platform/1

                platformsList[i].Url = ConfigURL.base_url + $"platform/{platformsList[i].Platform.Id}";
            }

            return platformsList;

        }

        public PlatformDetails GetById(int id)
        {
            PlatformDetails platformDetails = platformRepository.GetById(id);

            if (platformDetails == null)
            {
                return null;
            }

            for (int i = 0; i < platformDetails.Publishers.Count; i++)
            {
                PublisherSales publishersList = platformDetails.Publishers[i];
                publishersList.Url = ConfigURL.base_url + $"publisher/{publishersList.Publisher.Id}";
            }

            foreach (GameSales gamesList in platformDetails.Games)
            {
                gamesList.Url = ConfigURL.base_url + $"game/{gamesList.Game.Id}";
            }

            return platformDetails;
        }
    }
}
