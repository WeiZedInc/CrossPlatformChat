using Chat.API.Functions.User;
using Chat.API.Managers;
using Chat.API.Managers.User.Data;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneralUsersController : Controller
    {
        private IUserManager _userManager;
        public GeneralUsersController(IUserManager userManager) => _userManager = userManager;

        [HttpPost("GetByUsername")]
        public IActionResult GetByUsername(BaseRequest request)
        {
            GeneralUser? user = _userManager.GetGeneralUserByUsername(request.Login);

            if (user != null)
                return Ok(user);

            return BadRequest("Unexpected error at GeneralUsers controller!");
        }

    }
}
