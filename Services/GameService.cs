using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GameService
    {
        public GameRepository repository = new GameRepository();

        public List<GameSales> GetAll()
        {
            var gameList = repository.GetAll();

            for (int i = 0; i < gameList.Count; i++)
            {
                gameList[i].Url = ConfigURL.BASE_URL + $"platform/{gameList[i].game.Id}";
            }

            return gameList;
        }

        public GameDetails GetById(int id)
        {
            var gameById = repository.GetById(id);

            foreach (PublisherSales getPublishers in gameById.publisher)
            {
                getPublishers.Url = ConfigURL.BASE_URL + $"/publisher/{getPublishers.publisher.Id}";
            }

            foreach (PlatformSales getPlatforms in gameById.platform)
            {
                getPlatforms.Url = ConfigURL.BASE_URL + $"/publisher/{getPlatforms.platform.Id}";
            }

            foreach (Region_Sales getRegions in gameById.regionSales)
            {
                getRegions.Url = ConfigURL.BASE_URL + $"/publisher/{getRegions.region.Id}";
            }

            return gameById;
        }
    }
}
