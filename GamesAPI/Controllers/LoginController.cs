using Domain;
using Microsoft.AspNetCore.Mvc;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        [HttpPost]
        public IActionResult Login(Login login)
        {
            LoginService service = new LoginService();

            var isLogged = service.Login(login);

            if (isLogged == true)
            {
                return Ok();
            }
            else { return Unauthorized(); }

        }

        [HttpGet]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
