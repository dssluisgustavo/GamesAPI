using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Game
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
    }

    public class GameDetails
    {
        public Game Game { get; set; }

        public List<Region_Sales> Region { get; set; }
        public List<PlatformSales> Platform { get; set; }
        public List<PublisherSales> Publisher { get; set; }

        public string Url { get; set; }
        public double Sales { get; set; }
    }

    public class GameSales
    {
        public Game Game { get; set; }
        public string Url { get; set; }
        public double Sales { get; set; }
    }
}
