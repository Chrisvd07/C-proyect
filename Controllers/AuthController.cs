using ApplicationLayer.Service.TareaService.Auth;
using Domainlayer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest request)
        {
            var response = _authService.Authenticate(request);
            if (response == null)
                return Unauthorized();

            return Ok(response);
        }
    }
}
