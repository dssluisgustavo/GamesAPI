using Microsoft.AspNetCore.Mvc;
using Domain;
using Services;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route ("[controller]")]
    public class PublisherController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            PublisherService get = new PublisherService();
            var getAll = get.GetAll();

            return Ok(getAll);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            PublisherService getById= new PublisherService();
            var getPublisher = getById.GetById(id);

            if (getPublisher == null)
            {
                return BadRequest();
            }

            return Ok(getPublisher);
        }
    }
}
