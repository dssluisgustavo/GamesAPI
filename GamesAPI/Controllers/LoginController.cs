using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryEF.Models;
using Services;
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
            LoginData tokens = loginService.Login(login);

            if (tokens != null)
            {
                return Ok(tokens);
            }
            else { return Unauthorized(); }
        }

        [HttpPost("refresh")]
        public IActionResult LoginWithToken(LoginWithRefresh loginWithRefresh)
        {
            LoginData login = loginService.LoginWithRefresh(loginWithRefresh);

            if (login == null)
            {
                return Unauthorized();
            }
            return Ok(login);
        }

        [Authorize]
        [HttpGet("/logout")]
        public IActionResult Logout(string username)
        {
            loginService.Logout(username);

            return Ok();
        }
    }
}
