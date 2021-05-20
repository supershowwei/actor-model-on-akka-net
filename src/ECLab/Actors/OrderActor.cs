using System;
using System.Collections.Generic;
using System.Linq;
using Akka;
using Akka.Actor;
using ECLab.Model.Data;
using ECLab.Model.Messages;

namespace ECLab.Actors
{
    public class OrderActor : UntypedActor
    {
        protected override void PreStart()
        {
            Context.System.EventStream.Subscribe(this.Self, typeof(OrderPaid));

            base.PreStart();
        }

        protected override void OnReceive(object message)
        {
            message.Match()
                .With<OrderProducts>(msg => this.HandleOrderProducts(msg))
                .With<OrderPaid>(msg => this.OnOrderPaid(msg));
        }

        private void HandleOrderProducts(OrderProducts cmd)
        {
            var products = this.QueryProducts(cmd.ProductIDs);

            this.Sender.Tell(
                new OrderCreated { Order = new Order { Id = 1, Products = products, TotalPrice = products.Sum(p => p.Price) } });
        }

        private void OnOrderPaid(OrderPaid evt)
        {
            Console.WriteLine($"Order '{evt.Order.Id}' paid.");
            Console.WriteLine();
        }

        private List<Product> QueryProducts(List<int> productIDs)
        {
            return new() { new() { Id = 1, Name = "口罩", Price = 10 }, new() { Id = 2, Name = "酒精", Price = 20 } };
        }
    }
}