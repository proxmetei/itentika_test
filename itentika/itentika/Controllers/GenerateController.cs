using itentika.Models;
using itentika.Services;
using Microsoft.AspNetCore.Mvc;

namespace itentika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateController : ControllerBase
    {
        private readonly ILogger<GenerateController> _logger;
        public GenerateController(ILogger<GenerateController> logger)
        {
            _logger = logger;
        }
        [HttpPost(Name = "CreateEvent")]
        public IActionResult GenerateEventManually(int? type)
        {
            Event myEvent = type==null ? Generator.GenerateEvent() : Generator.GenerateEvent(type.Value);
            _logger.LogInformation(
            "GenerateController is working. Event: {eventStr}", myEvent.ToString());
            try
            {
                RequestGenerator.SendRequest(myEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Error while sending manual requset. Exception: {ex}", ex);
                return StatusCode(400);
            }
            return StatusCode(200);
        }
    }
}
