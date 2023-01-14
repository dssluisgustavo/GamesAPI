using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [Serializable]
    public class PlatformService
    {
        PlatformRepository repository = new PlatformRepository();

        public List<PlatformSales> GetAll()
        {
            var platList = repository.GetAll();

            for (int i = 0; i < platList.Count; i++)
            {
                //locallhost/006/platform/1

                platList[i].Url = ConfigURL.BASE_URL + $"platform/{platList[i].platform.Id}";
            }

            return platList;

        }

        public PlatformDetails GetById(int id)
        {
            var platformById = repository.GetById(id);

            for (int i = 0; i < platformById.publishers.Count; i++)
            {
                PublisherSales getPublishers = platformById.publishers[i];
                getPublishers.Url = ConfigURL.BASE_URL + $"publisher/{getPublishers.publisher.Id}";
            }

            foreach (GameSales getGames in platformById.games)
            {
                getGames.Url = ConfigURL.BASE_URL + $"game/{getGames.game.Id}";
            }

            return platformById;
        }
    }
}
