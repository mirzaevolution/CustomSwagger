using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace One.Write.Api.Controllers
{
    /// <summary>
    /// Write Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
                message = "Write",
                userInfo = new
                {
                    username = User.Identity.Name,
                    emal = User.FindFirstValue("email")
                }
            });
        }

        [HttpPost("{id}")]
        public IActionResult Post(string id)
        {
            return Ok(new
            {
                message = "Write",
                paramId = id,
                userInfo = new
                {
                    username = User.Identity.Name,
                    emal = User.FindFirstValue("email")
                }
            });
        }

        [HttpPost("{id}/{name}")]
        public IActionResult Post(string id, string name)
        {
            return Ok(new
            {
                message = "Write",
                parameters = new string[] { id, name },
                userInfo = new
                {
                    username = User.Identity.Name,
                    emal = User.FindFirstValue("email")
                }
            });
        }
    }
}
