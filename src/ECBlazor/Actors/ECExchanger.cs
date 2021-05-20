using System;
using Akka;
using Akka.Actor;
using ECBlazor.ViewModels;
using ECLab.Model.Messages;
using Newtonsoft.Json;

namespace ECBlazor.Actors
{
    public class ECExchanger : UntypedActor
    {
        private readonly ECViewModel viewModel;

        public ECExchanger(ECViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public static Props Props(ECViewModel viewModel)
        {
            return Akka.Actor.Props.Create<ECExchanger>(viewModel);
        }

        protected override void OnReceive(object message)
        {
            message.Match()
                .With<SearchProducts>(
                    msg =>
                        {
                            Context.ActorSelection("akka://ec/user/product").Tell(msg);
                        })
                .With<ProductsSearchResult>(
                    msg =>
                        {
                            this.viewModel.Products = JsonConvert.SerializeObject(msg.Products, Formatting.Indented);
                        })
                .With<OrderProducts>(
                    msg =>
                        {
                            Context.ActorSelection("akka://ec/user/order").Tell(msg);
                        })
                .With<OrderCreated>(
                    msg =>
                        {
                            this.viewModel.Order = JsonConvert.SerializeObject(msg.Order, Formatting.Indented);
                        })
                .With<PayForOrder>(
                    msg =>
                        {
                            Context.ActorSelection("akka://ec/user/payment").Tell(msg);
                        })
                .With<ProductDelivered>(
                    msg =>
                        {
                            this.viewModel.Delivery = JsonConvert.SerializeObject(msg.Products, Formatting.Indented);
                        });
        }
    }
}