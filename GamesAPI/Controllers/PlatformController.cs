using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route ("[controller]")]
    public class PlatformController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            PlatformService get = new PlatformService();
            var showAll = get.GetAll();

            return Ok(showAll);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            PlatformService getById = new PlatformService();
            var showById = getById.GetById(id);

            if(showById == null)
            {
                return BadRequest();
            }

            return Ok(showById);
        }
    }
}
