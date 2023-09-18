using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Entities.Concrete;
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


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User user)
        {
            var response = await _userService.AuthenticateUserAsync(user);
            return response.Success ? Ok(response) : BadRequest(response);
        }
       

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var response = await _userService.RegisterUserAsync(user);
            return response.Success ? Ok(response) : BadRequest(response);
        }


    }
}
