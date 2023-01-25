using Domain;
using RepositoryEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPlatformRepository
    {
        List<Platform> GetAll();

        Platform GetById(int id);
    }
}
