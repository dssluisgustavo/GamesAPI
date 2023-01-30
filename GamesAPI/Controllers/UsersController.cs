using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Services;
using Services.Interfaces;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        public UsersController(IUserService userServiceInterface)
        {
            userService = userServiceInterface;
        }

        [HttpPost]
        public IActionResult SignUp([FromBody] ValidUser user)
        {
            int login = userService.CreateUser(user);

            if(login == 0)
            {
                return BadRequest();
            }
            return Created($"\\user\\{login}", login);

        }
    }
}
