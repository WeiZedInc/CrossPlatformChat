using Chat.API.Controlls;
using Chat.API.Functions.User;
using Chat.API.Managers.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chat.API.Controllers.Chat
{
    [ApiController]
    [Route("[controller]")]
    public class FriendsController : Controller
    {
        private IUserManager _userManager;
        public FriendsController(IUserManager userManager) => _userManager = userManager;

        [HttpPost("GetFriends")]
        public IActionResult GetFriends(IBaseRequest request) 
        {
            int[]? friends = JsonConvert.DeserializeObject<int[]>(request.FriendsJSON);

            var response = _userManager.GetUserByID(friends[0]);
            if (response == null)
                return BadRequest(new { message = "Invalid username or password!" });

            return Ok(response);
        }
    }
}
