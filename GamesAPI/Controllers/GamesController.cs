using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : Controller
    {
        private readonly IGameService  gameService;
        public GamesController(IGameService gameServiceInterface)
        {
            gameService = gameServiceInterface;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        { 
            List<GameSales> allGames = gameService.GetAll();

            return Ok(allGames);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GameDetails game = gameService.GetById(id);

            if (game== null)
            {
                return BadRequest();
            }
            return Ok(game);
        }
    }
}
