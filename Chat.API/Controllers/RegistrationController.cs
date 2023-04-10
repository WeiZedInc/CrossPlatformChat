using Chat.API.Functions.User;
using Chat.API.Managers;
using Chat.API.Managers.User.Data;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
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
            var response = _userManager.Register(request.Login, request.HashedPassword);
            RegistrationStatus status = response.Item2;
            ClientResponse? user = response.Item1;

            if (user != null && status == RegistrationStatus.Success)
                return Ok(user);


            var message = status switch
            {
                RegistrationStatus.LoginOccupied => $"Login occupied!",
                RegistrationStatus.InvalidLogin => "Invalid login!",
                RegistrationStatus.InvalidPassword => "Invalid password!",
                _ => "Unexpected error at register controller!"
            };

            return BadRequest(message);
        }

    }
}
