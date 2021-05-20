using System;
using System.Collections.Generic;
using System.Linq;
using Akka;
using Akka.Actor;
using ECLab.Model.Data;
using ECLab.Model.Messages;

namespace ECLab.Actors
{
    public class InventoryActor : UntypedActor
    {
        private IActorRef logisticsActor;

        protected override void PreStart()
        {
            this.logisticsActor = Context.ActorOf(Props.Create<LogisticsActor>(), "logistics");

            base.PreStart();
        }

        protected override void OnReceive(object message)
        {
            message.Match().With<OrderPaid>(msg => this.OnOrderPaid(msg));
        }

        private void OnOrderPaid(OrderPaid evt)
        {
            this.DecreaseStock(evt.Order.Products);

            Console.WriteLine($"Products '{string.Join(",", evt.Order.Products.Select(x => x.Id))}' stock decreased.");
            Console.WriteLine();

            var deliveryProducts = new DeliveryProducts { Products = evt.Order.Products };

            this.logisticsActor.Forward(deliveryProducts);
            //this.logisticsActor.Tell(deliveryProducts, this.Sender);
        }

        private void DecreaseStock(List<Product> products)
        {
            // Do decrease stock
        }
    }
}