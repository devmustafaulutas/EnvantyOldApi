namespace envantyService.Controllers
{

    using envantyService.Service;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatbotService _chatbotService;

        public ChatController(ChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        [HttpGet("ask")]
        public async Task<IActionResult> Ask([FromQuery] string question)
        {
            try
            {
                var answer = await _chatbotService.GetChatbotAnswerAsync(question);
                return Ok(new { answer });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("ask2")]
        public async Task<IActionResult> Ask2([FromQuery] string question)
        {
            try
            {
                var answer = await _chatbotService.GetChatbotAnswer2Async(question);
                return Ok(new { answer });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}