using Microsoft.AspNetCore.Mvc;

namespace ServiceBus.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceBusController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMessage()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PostMessageToQueue()
        {

        }
    }
}
