using System;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Routing;
using ECLab.Actors;
using ECLab.Model.Messages;
using Newtonsoft.Json;

namespace ECLab
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var sys = ActorSystem.Create("ec");

            var productActor = sys.ActorOf(Props.Create<ProductActor>().WithRouter(new RoundRobinPool(10)), "product");
            var productActorSelection = sys.ActorSelection("akka://ec/user/product");

            var orderActor = sys.ActorOf(Props.Create<OrderActor>(), "order");

            var paymentActor = sys.ActorOf(Props.Create<PaymentActor>(), "payment");

            var inventoryActory = sys.ActorOf(Props.Create<InventoryActor>(), "inventory");

            Console.WriteLine("Input command:");

            string request;

            while (!(request = Console.ReadLine()).Equals("Quit", StringComparison.InvariantCultureIgnoreCase))
            {
                if (request.Equals("SearchProducts", StringComparison.InvariantCultureIgnoreCase))
                {
                    var msg = new SearchProducts { Keyword = "abc" };
                    //var msg = new SearchProducts("abc");

                    var searchResult = await productActor.Ask<ProductsSearchResult>(msg);
                    //var searchResult = await productActorSelection.Ask<ProductsSearchResult>(msg);

                    Console.WriteLine(JsonConvert.SerializeObject(searchResult.Products, Formatting.Indented));
                    Console.WriteLine();
                }
                else if (request.Equals("Order", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Please input products:");

                    var productIDs = Console.ReadLine().Split(',').Select(x => int.Parse(x)).ToList();

                    var orderCreated = await orderActor.Ask<OrderCreated>(new OrderProducts { ProductIDs = productIDs });

                    Console.WriteLine(JsonConvert.SerializeObject(orderCreated.Order, Formatting.Indented));
                    Console.WriteLine();
                }
                else if (request.Equals("Pay", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Please input order:");

                    var orderId = int.Parse(Console.ReadLine());

                    var productDelivered = await paymentActor.Ask<ProductDelivered>(new PayForOrder { OrderId = orderId });

                    Console.WriteLine("Receive Products:");
                    Console.WriteLine(JsonConvert.SerializeObject(productDelivered.Products, Formatting.Indented));
                    Console.WriteLine();
                }

                Console.WriteLine("Input command:");
            }
        }
    }
}