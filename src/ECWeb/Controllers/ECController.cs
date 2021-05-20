using System.Threading.Tasks;
using Akka.Actor;
using ECLab.Model.Messages;
using Microsoft.AspNetCore.Mvc;

namespace ECWeb.Controllers
{
    public class ECController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchProducts()
        {
            var searchResult = await ECSystem.Instance.ActorSelection("akka://ec/user/product").Ask<ProductsSearchResult>(new SearchProducts());

            return this.Json(searchResult.Products);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceAnOrder()
        {
            var msg = await ECSystem.Instance.ActorSelection("akka://ec/user/order").Ask<OrderCreated>(new OrderProducts());

            return this.Json(msg.Order);
        }

        [HttpPost]
        public async Task<IActionResult> PayForOrder()
        {
            var msg = await ECSystem.Instance.ActorSelection("akka://ec/user/payment").Ask<ProductDelivered>(new PayForOrder());

            return this.Json(msg.Products);
        }
    }
}