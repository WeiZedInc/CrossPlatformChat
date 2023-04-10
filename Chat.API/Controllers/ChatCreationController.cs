using Chat.API.Managers;
using Chat.API.Managers.Chat.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatCreationController : Controller
    {
        private IChatManager _chatManager;
        public ChatCreationController(IChatManager chatManager) => _chatManager = chatManager;

        [HttpPost("CreateNewChat")]
        public IActionResult CreateNewChat(ChatCreationRequest request)
        {
            var response = _chatManager.CreateNewChat(request);
            if (response == null)
                return BadRequest(new { message = "Error on creating new chat on API" });

            return Ok(response);
        }
    }
}
