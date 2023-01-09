using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        [HttpPost]
        public IActionResult SignUp([FromBody] User user)
        {
            UserService getinfos = new UserService();

            var userId = getinfos.CreateUser(user);

            return Created($"\\user\\{userId}", userId);

        }

        [HttpGet("forgotpassword.email={username}")]
        public IActionResult ForgotPassWord(string username)
        {
            UserService getUser = new UserService();

            var obj = getUser.CreateNewPassword(username);

            User user = new User();

            user.email = obj.email;

            if (user.email == obj.email)
            {
                return Ok(ConfigURL.BASE_URL + $"recoverpassword?username={username}");
            }
            return BadRequest();
        }
    }
}
