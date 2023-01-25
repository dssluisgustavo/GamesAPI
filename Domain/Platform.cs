using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PlatformDetails
    {
        public int PlatformId { get; set; }
        public string PlatformName { get; set; }
        public string PlatformUrl { get; set; }
        public decimal Sales { get; set; }

        public List<PublisherSales> Publishers { get; set; }
        public List<GameSales> Games { get; set; }

    }

    public class PlatformSales
    {
        public int PlatformId { get; set; }
        public string PlatformName { get; set; }
        public string PlatformUrl { get; set; }
        public decimal Sales { get; set; }
    }
}
