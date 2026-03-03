using Microsoft.AspNetCore.Mvc;
using NovaCore.Models.Request;
using NovaCore.Services.Interfaces;

namespace NovaCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoiceController : ControllerBase
    {
        private readonly IVoiceService _voiceService;

        public VoiceController(IVoiceService voiceService)
        {
            _voiceService = voiceService;
        }

        [HttpPost("process-text")]
        public IActionResult ProcessText([FromBody] VoiceRequest request)
        {
            var response = _voiceService.ProcessText(request);
            return Ok(response);
        }
    }
}