using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Publisher
    {
        public int Id;
        public string Name;
    }

    public class PublisherDetails
    {
        public Publisher publisher;
        public PlatformSales platform;
        public List <PlatformSales> platformList;


        public string Url;
    }

    public class PublisherSales
    {
        public Publisher publisher;
        public string Url;
        public double Sales;

    }
}
