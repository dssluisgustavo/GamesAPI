using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class PlatformDetails
    {
        public Platform Platform { get; set; }

        public List<PublisherSales> Publishers { get; set; }
        public List <GameSales> Games { get; set; }

        public string Url { get; set; }
        public double Sales { get; set; }

    }

    public class PlatformSales
    {
        public Platform Platform { get; set; }
        public double Sales { get; set; }
        public string Url { get; set; }
    }
}
