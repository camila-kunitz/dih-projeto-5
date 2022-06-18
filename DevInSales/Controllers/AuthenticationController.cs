using DevInSales.DTOs;
using DevInSales.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevInSales.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public ActionResult<string> Login(LoginDTO login)
        {
            var user = _userRepository.ValidarCredenciais(login.Email, login.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = _tokenService.GerarToken(user);

            return Ok(token);
        }
    }
}
