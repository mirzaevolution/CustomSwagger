using Microsoft.AspNetCore.Mvc;

namespace One.Read.Api.Controllers
{
    /// <summary>
    /// Read controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReadController : ControllerBase
    {
        /// <summary>
        /// Get endpoint for Read controller
        /// </summary>
        /// <returns>Message string</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                message = "Read"
            });
        }
    }
}
