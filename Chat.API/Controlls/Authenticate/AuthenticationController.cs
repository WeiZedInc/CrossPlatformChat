using Chat.API.Functions.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controlls.Authenticate
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private IUserFunction _userFunction;
        public AuthenticationController(IUserFunction userFunction) => _userFunction = userFunction;

        [HttpPost("Authenticate")]
        public IActionResult Authenticate(AuthenticateRequest request)
        {
            var response = _userFunction.Authenticate(request.Login, request.Password);
            if (response == null)
                return BadRequest(new { message = "Invalid username or password!" });

            return Ok(response);
        }

    }
}
