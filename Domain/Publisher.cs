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

    public class PublisherSales
    {
        public Publisher publisher = new Publisher();
        public string Url;
        public double Sales;

    }
}
