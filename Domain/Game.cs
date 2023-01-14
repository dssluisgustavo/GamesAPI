using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Game
    {
        public int Id;
        public string Name;
        public string Genre;
    }

    public class GameDetails
    {
        public Game game;

        public List<Region_Sales> regionSales;
        public List<PlatformSales> platform;
        public List<PublisherSales> publisher;

        public string Url;
        public double Sales;
    }

    public class GameSales
    {
        public Game game;
        public string Url;
        public double Sales;
    }
}
