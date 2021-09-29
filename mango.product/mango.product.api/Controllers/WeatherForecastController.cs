using mango.product.data.Context;
using Microsoft.AspNetCore.Mvc;

namespace mango.product.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ProductContext _productContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ProductContext productContext)
        {
            _logger = logger;
            _productContext = productContext;
            var isOpen = productContext.IsOpen();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {



            var p = _productContext.Products.ToList();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}