using Microsoft.AspNetCore.Mvc;
using Services;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            GameService game = new GameService();
            var showAll = game.GetAll();

            return Ok(showAll);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GameService gameById = new GameService();
            var getById = gameById.GetById(id);

            if (getById== null)
            {
                return BadRequest();
            }
            return Ok(getById);
        }
    }
}
