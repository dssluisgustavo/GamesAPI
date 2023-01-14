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
    public class PublisherService
    {
        PublisherRepository repository = new PublisherRepository();

        public List<PublisherSales> GetAll()
        {
            var publisherList = repository.GetAll();

            for (int i = 0; i < publisherList.Count; i++)
            {
                publisherList[i].Url = ConfigURL.BASE_URL + $"platform/{publisherList[i].publisher.Id}";
            }
            return publisherList;
        }

        public PublisherDetails GetById(int id)
        {
            var publisherById = repository.GetById(id);
            
            for (int i = 0; i<publisherById.platformList.Count; i++)
            {
                PlatformSales getPlatforms = publisherById.platformList[i];
                getPlatforms.Url = ConfigURL.BASE_URL + $"platforms{getPlatforms.platform.Id}";

            }

            return publisherById;

        }
    }
}
