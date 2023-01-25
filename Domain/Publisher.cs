using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PublisherDetails
    {
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string PublisherUrl { get; set; }
        public decimal Sales { get; set; }
        public PlatformSales Platform { get; set; }
        public List<PlatformSales> PlatformsList { get; set; }
    }

    public class PublisherSales
    {
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string PublisherUrl { get; set; }
        public decimal Sales { get; set; }

    }
}
