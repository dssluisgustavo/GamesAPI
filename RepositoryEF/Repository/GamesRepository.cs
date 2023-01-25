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
    public class GamesRepository : IGameRepository
    {
        private readonly PostgresContext context;
        public GamesRepository(PostgresContext _context)
        {
            context= _context;
        }

        public List<Game> GetAll()
        {
            List<Game> gamesList = context.Games
                .Include(g => g.GamePublishers).ThenInclude(g => g.GamePlatforms).ThenInclude(g => g.RegionSales).ToList();

            return gamesList;
        }

        public Game GetById(int id)
        {
            Game game = context.Games
                .Include(g => g.GamePublishers).ThenInclude(g => g.Publisher)
                .Include(g => g.GamePublishers).ThenInclude(g => g.GamePlatforms).ThenInclude(g => g.Platform)
                .Include(g => g.GamePublishers).ThenInclude (g => g.GamePlatforms). ThenInclude(g => g.RegionSales).ThenInclude(g => g.Region)
                .FirstOrDefault(g => g.Id == id) ;

            return game;
        }
    }
}
