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

        [HttpGet("forgotpassword.email={username}")]
        public IActionResult ForgotPassWord(string username)
        {
            ValidUser user = userService.ForgotPassword(username);

            ValidUser Registered = new ValidUser();

            Registered.Email = user.Email;

            if (Registered.Email == user.Email)
            {
                return Ok(ConfigURL.base_url + $"recoverpassword?username={username}");
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
