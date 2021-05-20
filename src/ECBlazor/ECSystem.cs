using System;
using Akka.Actor;
using Akka.Routing;
using ECLab.Actors;

namespace ECBlazor
{
    public class ECSystem
    {
        private static readonly Lazy<ActorSystem> Lazy = new Lazy<ActorSystem>(
            () =>
                {
                    var sys = ActorSystem.Create("ec");

                    sys.ActorOf(Props.Create<ProductActor>().WithRouter(new RoundRobinPool(10)), "product");
                    sys.ActorOf(Props.Create<OrderActor>(), "order");
                    sys.ActorOf(Props.Create<PaymentActor>(), "payment");
                    sys.ActorOf(Props.Create<InventoryActor>(), "inventory");

                    return sys;
                });

        private ECSystem()
        {
        }

        public static ActorSystem Instance => Lazy.Value;
    }
}