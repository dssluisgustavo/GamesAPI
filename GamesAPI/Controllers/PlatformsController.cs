using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Services;
using Services.Interfaces;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlatformsController : Controller
    {
        private readonly IPlatformService platformService;
        public PlatformsController(IPlatformService platformInterface)
        {
            platformService = platformInterface;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<PlatformSales> allPlatforms = platformService.GetAll();

            return Ok(allPlatforms);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            PlatformDetails platform = platformService.GetById(id);

            if (platform == null)
            {
                return BadRequest();
            }

            return Ok(platform);
        }
    }
}
