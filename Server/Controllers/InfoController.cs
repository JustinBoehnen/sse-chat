using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            TimeZoneInfo pt = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

            return Ok($"Started {TimeZoneInfo.ConvertTimeFromUtc(Info.Started, pt):MMMM d, yyyy h:mm:ss tt}");
        }
    }
}
