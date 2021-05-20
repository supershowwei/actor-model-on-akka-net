using System;
using Akka;
using Akka.Actor;
using ECLab.Model.Messages;
using Microsoft.AspNetCore.SignalR;

namespace ECSignalR.Actors
{
    public class SignalRClientWaiter : UntypedActor
    {
        private readonly IClientProxy caller;

        public SignalRClientWaiter(IClientProxy caller)
        {
            this.caller = caller;
        }

        public static Props Props(IClientProxy caller)
        {
            return Akka.Actor.Props.Create<SignalRClientWaiter>(caller);
        }

        protected override void OnReceive(object message)
        {
            message.Match()
                .With<ProductsSearchResult>(
                    msg =>
                        {
                            this.caller.SendAsync("ReceiveProductsSearchResult", msg.Products);
                        })
                .With<OrderCreated>(
                    msg =>
                        {
                            this.caller.SendAsync("OnOrderCreated", msg.Order);
                        })
                .With<ProductDelivered>(
                    msg =>
                        {
                            this.caller.SendAsync("OnProductDelivered", msg.Products);
                        });

            this.Self.Tell(PoisonPill.Instance);
        }
    }
}