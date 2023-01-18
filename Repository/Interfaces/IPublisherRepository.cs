using Domain;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPublisherRepository
    {
        List<PublisherSales> GetAll();

        PublisherDetails GetById(int id);
    }
}
