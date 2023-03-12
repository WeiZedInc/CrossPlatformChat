using Chat.API.Functions.User;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controlls.Register
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : Controller
    {
        private IUserManager _userManager;
        public RegistrationController(IUserManager userManager) => _userManager = userManager;

        [HttpPost("Register")]
        public IActionResult Register(BaseRequest request)
        {
            var response = _userManager.Register(request.Login, request.Password);

            if (response.Item1 != null && response.Item2 == RegistrationStatus.Success)
                return Ok(response);

            if (response.Item2 == RegistrationStatus.InvalidLogin) // add switch or smth to handle errors
                return BadRequest(new { message = "Invalid username or password!" });

            return BadRequest(new { message = "End method" });
        }

    }
}
