using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PublisherDetails
    {
        public Publisher Publisher { get; set; }
        public PlatformSales Platform { get; set; }
        public List <PlatformSales> PlatformsList { get; set; }


        public string Url { get; set; }
    }

    public class PublisherSales
    {
        public Publisher Publisher { get; set; }
        public string Url { get; set; }
        public double Sales { get; set; }

    }
}
