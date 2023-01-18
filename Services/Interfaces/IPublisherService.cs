using Domain;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPublisherService
    {
        List<PublisherSales> GetAll();

        PublisherDetails GetById(int id);
    }
}
