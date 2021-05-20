using System;
using Akka;
using Akka.Actor;
using ECLab.Model.Messages;
using ECWinForm.ViewModels;
using Newtonsoft.Json;

namespace ECWinForm.UIWorkers
{
    public class Form1Exchanger : UntypedActor
    {
        private readonly Form1ViewModel viewModel;

        public Form1Exchanger(Form1ViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public static Props Props(Form1ViewModel viewModel)
        {
            return Akka.Actor.Props.Create<Form1Exchanger>(viewModel);
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