using Microsoft.AspNetCore.Mvc;
using ServiceBus.Application.Requests;
using ServiceBus.Interfaces.Services;

namespace ServiceBus.WebApi.Controllers
{
    [ApiController]
    [Route("ServiceBus")]
    public class ServiceBusController : ControllerBase
    {
        private readonly IServiceBusService serviceBusService;

        public ServiceBusController(IServiceBusService serviceBusService)
        {
            this.serviceBusService = serviceBusService;
        }

        [HttpGet]
        public IActionResult GetMessage()
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult PostMessageToQueue([FromBody] PostMessageToQueueRequest request)
        {
            serviceBusService.PostChatMessageToQueueAsync(request);

            return Created(string.Empty, string.Empty);
        }
    }
}
