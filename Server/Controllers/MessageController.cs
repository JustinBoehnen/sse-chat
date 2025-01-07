using Microsoft.AspNetCore.Mvc;
using Server.Services;
using System.Text.Json;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController(IMessageService messageService) : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] Message message)
        {
            messageService.NotifyNewMessageAvailable(message);

            return Ok();
        }

        [HttpGet]
        public async Task Get()
        {
            HttpContext.Response.Headers.Append("Content-Type", "text/event-stream");

            messageService.RegisterListener(this);

            try
            {
                while (!HttpContext.RequestAborted.IsCancellationRequested)
                {
                    Message message = await messageService.WaitForNewMessageAsync(this, HttpContext.RequestAborted);

                    await HttpContext.Response.WriteAsync("data: ");
                    await JsonSerializer.SerializeAsync(HttpContext.Response.Body, message);
                    await HttpContext.Response.WriteAsync("\n\n");
                    await HttpContext.Response.Body.FlushAsync();
                }
            }
            finally
            {
                messageService.UnregisterListener(this);
            }
        }
    }
}
