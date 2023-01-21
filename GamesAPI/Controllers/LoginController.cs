using Domain;
using Microsoft.AspNetCore.Authorization;
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
            string token = loginService.Login(login);

            if (!string.IsNullOrEmpty(token))
            {
                return Ok(token);
            }
            else { return Unauthorized(); }
        }

        [Authorize]
        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
