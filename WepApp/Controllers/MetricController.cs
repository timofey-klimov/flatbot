using Infrastructure.Interfaces.Cian;
using Microsoft.AspNetCore.Mvc;

namespace WepApp.Controllers
{
    [Route("api/metric")]
    public class MetricController : ControllerBase
    {
        public MetricController(ICianService service)
        {

        }

        [HttpGet]
        public void Get()
        {

        }

    }
}
