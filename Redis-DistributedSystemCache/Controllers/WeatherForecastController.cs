using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Redis_DistributedSystemCache.Helper;
using System.Threading.Tasks;

namespace Redis_DistributedSystemCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IDistributedCache _distributedCache { get; }
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IDistributedCache distributedCache)
        {
            _logger = logger;
            _distributedCache = distributedCache;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            List<WeatherForecast>? weather;
            string recordKey = $"Weather";
            weather = await _distributedCache.GetEntryAsync<List<WeatherForecast>>(recordKey);
            if (weather == null)
            {
                weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
               .ToList();
                await _distributedCache.SetEntryAsync(recordKey, weather);
            }
            HttpContext.Session.SetString("session1", "session1 text2");
            return weather;
        }
    }
}
