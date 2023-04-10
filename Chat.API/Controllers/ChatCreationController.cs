using Chat.API.Managers.Chat;
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
            var id = _chatManager.CreateNewChat(request);
            if (id == -1)
                return BadRequest(new { message = "Error on creating new chat on API" });

            return Ok(id);
        }
    }
}
