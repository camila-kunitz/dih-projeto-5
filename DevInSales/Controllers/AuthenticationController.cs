using DevInSales.DTOs;
using DevInSales.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public ActionResult<string> Login(LoginDTO login)
        {
            var user = _userRepository.ValidarCredenciais(login.Email, login.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok();
        }
    }
}
