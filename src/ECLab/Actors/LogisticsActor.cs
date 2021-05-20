using System.Collections.Generic;
using Akka;
using Akka.Actor;
using ECLab.Model.Data;
using ECLab.Model.Messages;

namespace ECLab.Actors
{
    public class LogisticsActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            message.Match().With<DeliveryProducts>(msg => this.HandleDeliveryProducts(msg));
        }

        private void HandleDeliveryProducts(DeliveryProducts cmd)
        {
            this.Delivery(cmd.Products);
        }

        private void Delivery(List<Product> products)
        {
            this.Sender.Tell(new ProductDelivered { Products = products });
        }
    }
}