using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Region_Sales
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public string Url { get; set; }
        public decimal SalesValue { get; set; }
    }
}
