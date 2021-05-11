using System.Threading.Tasks;
using Cleverbit.CodingTask.Application.DTO.Authentication;
using Cleverbit.CodingTask.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cleverbit.CodingTask.Host.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : BaseController {

        private readonly IAuthenticationService authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService) {
            this.authenticationService = authenticationService;
        }

        // POST: api/authentication/sign-in
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInInput input) {

            if (!ModelState.IsValid) {
                return BadRequest();
            }

            await authenticationService.SignIn(input);

            return Ok();
        }

        // GET: api/authentication/sign-out
        [HttpGet("sign-out")]
        public IActionResult SignOut() {
            // todo: Implement in the future when authentication mechanism has changed

            return Ok();
        }
    }
}
