using Microsoft.AspNetCore.Mvc;

namespace dotnet_api_knowledge_check.Controllers
{
    //TODO[<What should be here>]
    [Route("sample")] // Can you write how your api url looks like? and What is the alternative way we can configure route?
    public class SampleController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateWeather(/*TODO:[<What could be here>]*/ WeatherForecast weatherForecast)
        {
            // This is dummy but for actual POST request we should add different options.
            return Ok();
            /*TODO: <What could be return type here>(weatherForecast)*/
        }

        [HttpGet("{weatherid:int}")]
        public int get(/*TODO:[<What could be here>]*/ int weatherid)
        {
            return 0;
        }
    }
}
