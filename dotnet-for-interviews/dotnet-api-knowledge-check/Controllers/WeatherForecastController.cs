using Microsoft.AspNetCore.Mvc;

namespace dotnet_api_knowledge_check.Controllers
{
    [ApiController] // Do we really required this. If yes then where it helps?
    [Route("[controller]")] // vs [Route("weather")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpPost]
        // public IActionResult CreateWeather([FromBody] WeatherForecast weatherForecast)
        // Question: [FromBody] - When this one is not required to map
        //vs
        public IActionResult CreateWeather(WeatherForecast weatherForecast)
        {
            return Ok(weatherForecast);
        }

        [HttpGet("{weatherid:int}")]
        // public int get([FromRoute] int weatherid)
        // Question: [FromRoute] - When this one is not required to map?
        //vs
        public int get(int weatherid)
        {
            return 0;

        }
    }
}
