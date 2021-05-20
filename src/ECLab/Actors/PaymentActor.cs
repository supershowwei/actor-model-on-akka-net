using System.Collections.Generic;
using System.Linq;
using Akka;
using Akka.Actor;
using ECLab.Model.Data;
using ECLab.Model.Messages;

namespace ECLab.Actors
{
    public class PaymentActor : UntypedActor 
    {
        protected override void OnReceive(object message)
        {
            message.Match().With<PayForOrder>(msg => this.HandlePayForOrder(msg));
        }

        private void HandlePayForOrder(PayForOrder cmd)
        {
            var order = this.QueryOrder(cmd.OrderId);

            var orderPaid = new OrderPaid { Order = order };

            // like Forward
            Context.ActorSelection("akka://ec/user/inventory").Tell(orderPaid, this.Sender);

            Context.System.EventStream.Publish(orderPaid);
        }

        private Order QueryOrder(int orderId)
        {
            var products = new List<Product> { new() { Id = 1, Name = "口罩", Price = 10 }, new() { Id = 2, Name = "酒精", Price = 20 } };

            return new() { Id = orderId, Products = products, TotalPrice = products.Sum(p => p.Price) };
        }
    }
}