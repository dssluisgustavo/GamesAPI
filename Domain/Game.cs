using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class GameDetails
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public string GameGenre { get; set; }
        public string GameUrl { get; set; }
        public decimal Sales { get; set; }

        public List<Region_Sales> Region { get; set; }
        public List<PlatformSales> Platform { get; set; }
        public List<PublisherSales> Publisher { get; set; }

    }

    public class GameSales
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public string GameGenre { get; set; }
        public string GameUrl { get; set; }
        public decimal Sales { get; set; }
    }
}
