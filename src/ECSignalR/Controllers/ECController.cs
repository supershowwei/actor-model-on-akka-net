using Microsoft.AspNetCore.Mvc;

namespace ECSignalR.Controllers
{
    public class ECController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}