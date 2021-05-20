using System;
using Akka;
using Akka.Actor;
using ECLab.Model.Messages;
using ECSignalR.Messages;

namespace ECSignalR.Actors
{
    public class SignalRExchanger : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            message.Match()
                .With<SignalRMessage>(
                    msg =>
                        {
                            this.HandleECMessage(msg.Message, Context.ActorOf(SignalRClientWaiter.Props(msg.Caller)));
                        });
        }

        private void HandleECMessage(object message, IActorRef clientWaiter)
        {
            message.Match()
                .With<SearchProducts>(msg => Context.ActorSelection("akka://ec/user/product").Tell(msg, clientWaiter))
                .With<OrderProducts>(msg => Context.ActorSelection("akka://ec/user/order").Tell(msg, clientWaiter))
                .With<PayForOrder>(msg => Context.ActorSelection("akka://ec/user/payment").Tell(msg, clientWaiter));
        }
    }
}