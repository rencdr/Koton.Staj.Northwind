using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Koton.Staj.Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly string _jwtSecretKey;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _jwtSecretKey = configuration["JwtSecretKey"];
        }

        [HttpPost("/api/authenticate")]
        public IActionResult Authenticate(User user)
        {
            var response = _userService.AuthenticateUser(user);
            return Ok(response); 

        }

        [HttpPost("/api/register")]
        public IActionResult Register(User user)
        {
            var response = _userService.RegisterUser(user);
            return Ok(response);

        }


    }
}
