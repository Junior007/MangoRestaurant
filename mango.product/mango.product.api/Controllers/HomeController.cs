using Microsoft.AspNetCore.Mvc;

namespace mango.product.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        public IActionResult Index()
        {
            return Ok();
        }
    }
}
