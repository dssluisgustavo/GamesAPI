using Domain;
using RepositoryEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPublisherRepository
    {
        List<Publisher> GetAll();

        Publisher GetById(int id);
    }
}
