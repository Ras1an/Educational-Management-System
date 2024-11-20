using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Controllers
{
    public class InfraController : Controller
    {
        protected readonly HttpClient _client;

        public InfraController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7209/api");
    }

}
}
