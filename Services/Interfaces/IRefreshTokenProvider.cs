using Domain;
using RepositoryEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRefreshTokenProvider
    {
        string NewToken(User user);
    }
}
