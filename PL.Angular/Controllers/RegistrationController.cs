using Microsoft.AspNetCore.Mvc;
using PL.Angular.Models;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("registration")]
    public class RegistrationController : ControllerBase
    {
        public RegistrationController() 
        {

        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegisterModel registerModel)
        {
            return Ok(registerModel);
        }
    }
}
