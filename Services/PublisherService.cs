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
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository publisherRepository;
        public PublisherService(IPublisherRepository publisherRepositoryInterface)
        {
            publisherRepository = publisherRepositoryInterface;
        }

        public List<PublisherSales> GetAll()
        {
            List<PublisherSales> publisherList = publisherRepository.GetAll();

            for (int i = 0; i < publisherList.Count; i++)
            {
                publisherList[i].Url = ConfigURL.base_url + $"publisher/{publisherList[i].Publisher.Id}";
            }
            return publisherList;
        }

        public PublisherDetails GetById(int id)
        {
            PublisherDetails publisherDetails = publisherRepository.GetById(id);

            if (publisherDetails == null)
            {
                return null;
            }

            for (int i = 0; i < publisherDetails.PlatformsList.Count; i++)
            {
                PlatformSales platformsList = publisherDetails.PlatformsList[i];
                platformsList.Url = ConfigURL.base_url + $"publisher/{platformsList.Platform.Id}";

            }

            return publisherDetails;

        }
    }
}
