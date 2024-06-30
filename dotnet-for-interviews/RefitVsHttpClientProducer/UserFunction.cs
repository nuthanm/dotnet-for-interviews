using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using RefitVsHttpClientProducer.Models;

namespace RefitVsHttpClientProducer
{
    public class UserFunction
    {
        private readonly ILogger<UserFunction> _logger;

        public UserFunction(ILogger<UserFunction> logger)
        {
            _logger = logger;
        }

        [Function("GetUser")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "getUser/{id}")] HttpRequest req, int id)
        {
            return new OkObjectResult(new User() { Id = id, Name = "nani" });
        }
    }
}
