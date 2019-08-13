using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StartupConfiguration.App;

namespace StartupConfiguration.FunctionApp
{
    public class Function1
    {
        private readonly IAppService _service;

        public Function1(IAppService service)
        {
            _service = service;
        }

        [FunctionName("Function1")]
        public void Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequest req,
            ILogger log)
        {
            _service.Run();
        }
    }
}
