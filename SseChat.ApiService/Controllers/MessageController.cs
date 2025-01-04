using Microsoft.AspNetCore.Mvc;
using SseChat.ApiService.Services;
using System.Text.Json;

namespace SseChat.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // Server-sent event endpoint, continuously writes response body buffer until request is cancelled
        [HttpGet]
        public async Task Get()
        {
            // Set EventSource content type
            HttpContext.Response.Headers.Append("Content-Type", "text/event-stream");

            while(!HttpContext.RequestAborted.IsCancellationRequested)
            {
                var message = await _messageService.WaitForNewMessageAsync(HttpContext.RequestAborted);

                await HttpContext.Response.WriteAsync("data: ");
                await JsonSerializer.SerializeAsync(HttpContext.Response.Body, message);
                await HttpContext.Response.WriteAsync("\n\n");
                await HttpContext.Response.Body.FlushAsync();

                _messageService.Reset();
            }
        }
    }
}
