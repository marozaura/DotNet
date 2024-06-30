using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public IActionResult Get()
        {
            GetBasePath();
            var rng = new Random();
            var collection = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();

            return Ok(collection);
        }
        private string GetBasePath()
        {
            var currentDirectoryPath = Directory.GetCurrentDirectory();
            var parentDirectoryPath = Directory.GetParent(currentDirectoryPath).FullName;
            var basePath = Path.Combine(parentDirectoryPath, "AspTestProject");

            return basePath;
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public string Summary { get; set; }
        public int TemperatureC { get; set; }
    }
}
