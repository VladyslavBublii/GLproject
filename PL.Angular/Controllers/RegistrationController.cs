using Microsoft.AspNetCore.Mvc;
using PL.Angular.Models;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("registration")]
    public class RegistrationController : ControllerBase
    {
        RegistrationController() 
        {

        }

        [HttpPost("registration")]
        public IActionResult Registration(RegisterModel registerModel)
        {
            return Ok(registerModel);
        }
    }
}
