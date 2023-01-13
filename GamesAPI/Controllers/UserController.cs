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

            var user = getUser.ForgotPassword(username);

            User userEmail = new User();

            userEmail.Email = user.Email;

            if (userEmail.Email == user.Email)
            {
                return Ok(ConfigURL.BASE_URL + $"recoverpassword?username={username}");
            }
            return BadRequest();
        }

       /* [HttpPut]
        public IActionResult RecoverPassword (string username)
        {
            UserService getUser = new UserService();

            var user = getUser.RecoverPassword(username);

            User userForPassword = new User();

            user.password = getUser.RecoverPassword(password);
            
            if (userForPassword.username == user.username)
            {

            }
        }*/
    }
}
