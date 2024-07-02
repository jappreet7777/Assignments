using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAutheticationServices;
using UserAuthetictionDomain.Models;

namespace AuthenticationMiddleware.Controllers
{
    public class AuthenticationController:ControllerBase
    {
        private readonly AutheticationService _autheticationService;

        public AuthenticationController(AutheticationService autheticationService)
        {
            _autheticationService = autheticationService;
        }

        [AllowAnonymous]
        [HttpPost("AutheticateUser")]
        [Route("api/v2/login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = _autheticationService.Authenticate(userLogin);

            if (user != null)
            {
                var token = _autheticationService.Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }

    }
}
