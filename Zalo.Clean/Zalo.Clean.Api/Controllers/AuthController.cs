using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zalo.Clean.Application.Contracts.Identity;
using Zalo.Clean.Application.Modules.Identity;

namespace Zalo.Clean.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authenticationService;

        public AuthController(IAuthService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return Ok(await authenticationService.Login(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            return Ok(await authenticationService.Register(request));
        }


    }
}
