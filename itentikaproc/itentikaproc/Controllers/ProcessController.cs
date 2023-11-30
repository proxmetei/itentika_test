using itentikaproc.Models;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using itentikaproc.Services;
using itentikaproc.Repositories;
using System.Text.Json;

namespace itentikaproc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly ILogger<ProcessController> _logger;
        private readonly ProcessingService _service;
        private readonly ProcessingRepo _repos;
        public ProcessController(ILogger<ProcessController> logger, ProcessingService service, ProcessingRepo repos)
        {
            _logger = logger;
            _service = service;
            _repos = repos;
        }
        [HttpPost]
        public IActionResult ProcessEvent(Event myEvent)
        {
            _logger.LogInformation(
                "Event is accepted. Event: {eventStr}", myEvent.ToString());
            _service.processEvent(myEvent);
            return Ok();
        }
        [HttpGet]
        public JsonResult GetIncidents(bool sortByTime=true, bool sortByType=false, int pageNum=0, int pageSize=5)
        {
            return new JsonResult(_repos.GetAllIncidents(sortByTime, sortByType, pageNum, pageSize));
        }
    }
}
