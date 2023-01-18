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
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;
        public GameService(IGameRepository gameRepositoryInterface)
        {
            gameRepository = gameRepositoryInterface;
        }

        public List<GameSales> GetAll()
        {
            List<GameSales> gamesList = gameRepository.GetAll();

            for (int i = 0; i < gamesList.Count; i++)
            {
                gamesList[i].Url = ConfigURL.base_url + $"game/{gamesList[i].Game.Id}";
            }

            return gamesList;
        }

        public GameDetails GetById(int id)
        {
            GameDetails gameDetails = gameRepository.GetById(id);

            if (gameDetails == null)
            {
                return null;
            }

            foreach (PublisherSales publishersList in gameDetails.Publisher)
            {
                publishersList.Url = ConfigURL.base_url + $"/gamepublisher/{publishersList.Publisher.Id}";
            }

            foreach (PlatformSales platformsList in gameDetails.Platform)
            {
                platformsList.Url = ConfigURL.base_url + $"/gameplatform/{platformsList.Platform.Id}";
            }

            foreach (Region_Sales regionsList in gameDetails.Region)
            {
                regionsList.Url = ConfigURL.base_url + $"/gameregion/{regionsList.Region.Id}";
            }

            return gameDetails;
        }
    }
}
