using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Sales
    {
        public double SalesValue { get; set; }
    }
    public class Region_Sales
    {
        public Region Region { get; set; }
        public string Url { get; set; }
        public double SalesValue { get; set; }
    }
}
