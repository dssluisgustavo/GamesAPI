using Microsoft.AspNetCore.Mvc;
using Domain;
using Services;
using Repository.Interfaces;
using Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishersController : Controller
    {
        private readonly IPublisherService publisherService;
        public PublishersController(IPublisherService publisherInterface)
        {
            publisherService = publisherInterface;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<PublisherSales> allPublishers = publisherService.GetAll();

            return Ok(allPublishers);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            PublisherDetails publisher = publisherService.GetById(id);

            if (publisher == null)
            {
                return BadRequest();
            }

            return Ok(publisher);
        }
    }
}
