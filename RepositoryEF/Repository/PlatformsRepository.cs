using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using RepositoryEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryEF.Repository
{
    public class PlatformsRepository : IPlatformRepository
    {
        private readonly PostgresContext _context;

        public PlatformsRepository(PostgresContext context)
        {
            _context = context;
        }

        public List<Platform> GetAll()
        {
            List<Platform> platformsList = _context.Platforms
                .Include(p => p.GamePlatforms).ThenInclude( p => p.RegionSales).ToList();

            return platformsList;
        }

        public Platform GetById(int id)
        {
            Platform platform = _context.Platforms
                .Include(p => p.GamePlatforms).ThenInclude(gp => gp.GamePublishers).ThenInclude(gp => gp.Game)
                .Include(p => p.GamePlatforms).ThenInclude(gp => gp.GamePublishers).ThenInclude(gp => gp.Publisher)
                .FirstOrDefault(p => p.Id == id);

            return platform;
        }
    }
}
