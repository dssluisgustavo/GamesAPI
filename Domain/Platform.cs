using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Platform
    {
        public int Id;
        public string Name;

    }

    public class PlatformDetails
    {
        public Platform platInfos;

        public List<PublisherSales> publishers;
        public List <Game> games;

        public string Url;
        public double Sales;

    }

    public class PlatformSales
    {
        public Platform platform;
        public double Sales;
        public string Url;
    }
}
