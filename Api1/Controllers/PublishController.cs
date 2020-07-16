using Api1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : ControllerBase
    {

        public PublishController()
        {
        }
        [Route("~/ef/transaction")]
        public IActionResult EntityFrameworkWithTransaction([FromServices] IAppService appService)
        {
            appService.InsertTesttable();
            return Ok("ef ok");
        }
    }
}
