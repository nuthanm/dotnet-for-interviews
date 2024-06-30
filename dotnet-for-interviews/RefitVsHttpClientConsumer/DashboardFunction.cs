using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using RefitVsHttpClientConsumer.Contracts;

namespace RefitVsHttpClientConsumer
{
    public class DashboardFunction
    {
        private readonly ILogger<DashboardFunction> _logger;
        private readonly IUserMangementService _userMgtService;

        public DashboardFunction(
            ILogger<DashboardFunction> logger,
            IUserMangementService userMgtService)

        {
            _logger = logger;
            _userMgtService = userMgtService;
        }

        [Function("LoadUsers")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "loadusers/{id}")] HttpRequest req, int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:7042/api/");
                var response = await httpClient.GetAsync($"getUser/{id}");
                var responseInJson = response.Content.ReadAsStringAsync();
                return new OkObjectResult(responseInJson);
            }
        }

        [Function("LoadUsersUsingRefit")]
        public async Task<IActionResult> GetUsersUsingRefitAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "LoadUsersUsingRefit/{id}")] HttpRequest req, int id)
        {

            var response = await _userMgtService.GetUser(id);
            return new OkObjectResult(response);

        }
    }
}
