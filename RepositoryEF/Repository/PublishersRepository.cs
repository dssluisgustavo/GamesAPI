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
    public class PublishersRepository : IPublisherRepository
    {
        private readonly PostgresContext context;
        public PublishersRepository(PostgresContext _context)
        {
            context = _context;
        }

        public List<Publisher> GetAll()
        {
            List<Publisher> publishersList = context.Publishers
                .Include(p => p.GamePublishers).ThenInclude(p => p.GamePlatforms).ThenInclude(p => p.RegionSales).ToList();

            return publishersList;
        }

        public Publisher GetById(int id)
        {
            Publisher publisher = context.Publishers
                .Include(p => p.GamePublishers).ThenInclude(p => p.GamePlatforms).ThenInclude(p => p.Platform)
                .Include(p => p.GamePublishers).ThenInclude(p => p.GamePlatforms).ThenInclude(p => p.RegionSales)
                .FirstOrDefault(p => p.Id == id);

            return publisher;
        }
    }
}
