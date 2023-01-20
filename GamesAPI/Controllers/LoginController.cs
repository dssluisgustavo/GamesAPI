using Domain;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILoginService loginService;
        public LoginController(ILoginService loginServiceInterface)
        {
            loginService = loginServiceInterface;
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            bool isLogged = loginService.Login(login);

            if (isLogged == true)
            {
                return Ok();
            }
            else { return Unauthorized(); }
        }

        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
