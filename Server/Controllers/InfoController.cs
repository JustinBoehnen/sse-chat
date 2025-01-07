using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok($"API started {Info.Started.ToOffset(TimeSpan.FromHours(-8))}");
        }
    }
}
