using Domain;
using NPOI.SS.Formula.Functions;
using Repository;
using Repository.Interfaces;
using RepositoryEF.Models;
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
            List<Publisher> publisherList = publisherRepository.GetAll();
            List<PublisherSales> publisherSalesList = new List<PublisherSales>();

            foreach (Publisher publisher in publisherList)
            {
                PublisherSales publisherConversion = new PublisherSales();

                publisherConversion.PublisherId = publisher.Id;
                publisherConversion.PublisherName = publisher.PublisherName;
                publisherConversion.PublisherUrl = ConfigURL.base_url + $"publishers/{publisher.Id}";

                decimal sales = 0;

                foreach (GamePublisher gp in publisher.GamePublishers)
                {
                    foreach (GamePlatform gp2 in gp.GamePlatforms)
                    {
                        foreach (RegionSale rs in gp2.RegionSales)
                        {
                            sales += rs.NumSales.Value;
                        }
                    }
                }
                publisherConversion.Sales = sales;

                publisherSalesList.Add(publisherConversion);
            }

            return publisherSalesList;
        }

        public PublisherDetails GetById(int id)
        {
            Publisher publisher = publisherRepository.GetById(id);

            PublisherDetails publisherDetails = new PublisherDetails();

            publisherDetails.PlatformsList = new List<PlatformSales>();


            publisherDetails.PublisherId = publisher.Id;
            publisherDetails.PublisherName = publisher.PublisherName;
            publisherDetails.PublisherUrl = ConfigURL.base_url + $"publisher/{publisher.Id}";

            decimal sales = 0;

            foreach (GamePublisher gp in publisher.GamePublishers)
            {
                foreach (GamePlatform gp2 in gp.GamePlatforms)
                {
                    foreach (RegionSale rs in gp2.RegionSales)
                    {
                        sales += rs.NumSales.Value;
                    }
                }
            }
            publisherDetails.Sales = sales;


            foreach (GamePublisher gp in publisher.GamePublishers)
            {
                foreach (GamePlatform gp2 in gp.GamePlatforms)
                {
                    if (!publisherDetails.PlatformsList.Any(p => p.PlatformId == gp2.Platform.Id))
                    {
                        PlatformSales platformElement = new PlatformSales();

                        platformElement.PlatformId = gp2.Platform.Id;
                        platformElement.PlatformName = gp2.Platform.PlatformName;
                        platformElement.PlatformUrl = ConfigURL.base_url + $"platform/{gp2.Platform.Id}";

                        publisherDetails.PlatformsList.Add(platformElement);
                    }
                   
                }
            }

            if (publisherDetails == null)
            {
                return null;
            }

            return publisherDetails;
        }
    }
}
