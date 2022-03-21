using Microsoft.AspNetCore.Mvc;

namespace One.Write.Api.Controllers
{
    /// <summary>
    /// Write Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WriteController : ControllerBase
    {
        /// <summary>
        /// Post endpoint for Write Controller
        /// </summary>
        /// <returns>Message string</returns>
        [HttpPost]
        public IActionResult Post()
        {
            return Ok(new
            {
                message = "Write"
            });
        }
    }
}
