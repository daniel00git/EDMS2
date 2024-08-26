using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortcutController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateShortcut([FromBody] byte[] documentBytes)
        {
            // Create the shortcut file
            string shortcutPath = @"C:\Users\Administrator\Downloads\WebApplication2\WebApplication2";

            return File(shortcutPath, "application/lnk");
        }
    }
}
