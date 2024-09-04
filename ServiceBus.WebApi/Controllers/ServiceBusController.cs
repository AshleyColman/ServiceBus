using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using ServiceBus.Application.Requests;
using ServiceBus.Interfaces.Services;

namespace ServiceBus.WebApi.Controllers
{
    [ApiController]
    [Route("ServiceBus")]
    public class ServiceBusController(IServiceBusService serviceBusService) : ControllerBase
    {
        [HttpGet("PeekMessage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServiceBusReceivedMessage?>> PeekMessage()
        {
           ServiceBusReceivedMessage? message = await serviceBusService.PeekMessageAsync();

            return message is null ? BadRequest() : Ok(message);
        }

        [HttpGet("CompleteMessage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServiceBusReceivedMessage?>> CompleteMessage()
        {
            ServiceBusReceivedMessage? message = await serviceBusService.CompleteMessageAsync();

            return message is null ? BadRequest() : Ok(message);
        }

        [HttpPost("AddMessage")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddMessage([FromBody] AddMessageRequest request)
        {
            await serviceBusService.AddMessageAsync(request);

            return Created(string.Empty, string.Empty);
        }

        [HttpPost("BatchAddMessages")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> BatchAddMessages([FromBody] BatchAddMessagesRequest request)
        {
            if (request.Messages is null)
                return BadRequest();

            ICollection<ServiceBusMessage> result = await serviceBusService.BatchAddMessagesAsync(request.Messages);

            return Created(string.Empty, result);
        }
    }
}
