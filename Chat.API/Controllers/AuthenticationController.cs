using Chat.API.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private IUserManager _userManager;
        public AuthenticationController(IUserManager userManager) => _userManager = userManager;

        [HttpPost("Authenticate")]
        public IActionResult Authenticate(BaseRequest request)
        {
            var response = _userManager.Authenticate(request.Login, request.HashedPassword);
            if (response == null)
                return BadRequest(new { message = "Invalid username or password!" });

            return Ok(response);
        }

    }
}
