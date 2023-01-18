using Domain;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPlatformService
    {
        List<PlatformSales> GetAll();

        PlatformDetails GetById(int id);
    }
}
