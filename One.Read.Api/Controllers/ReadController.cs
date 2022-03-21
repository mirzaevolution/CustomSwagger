using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace One.Read.Api.Controllers
{
    /// <summary>
    /// Read controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
                message = "Read",
                userInfo = new
                {
                    username = User.Identity.Name,
                    emal = User.FindFirstValue("email")
                }
            });
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(new
            {
                message = "Read",
                paramId = id,
                userInfo = new
                {
                    username = User.Identity.Name,
                    emal = User.FindFirstValue("email")
                }
            });
        }

        [HttpGet("{id}/{name}")]
        public IActionResult Get(string id, string name)
        {
            return Ok(new
            {
                message = "Read",
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
