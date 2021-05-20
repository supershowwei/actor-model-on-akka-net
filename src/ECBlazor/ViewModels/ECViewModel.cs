using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Akka.Actor;
using ECBlazor.Actors;
using ECBlazor.Annotations;
using ECLab.Model.Messages;

namespace ECBlazor.ViewModels
{
    public class ECViewModel : INotifyPropertyChanged, IDisposable
    {
        private readonly IActorRef ecExchanger;
        private string products;
        private string order;
        private string delivery;

        public ECViewModel()
        {
            this.ecExchanger = ECSystem.Instance.ActorOf(ECExchanger.Props(this));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Products
        {
            get => this.products;
            set
            {
                if (value == this.products) return;

                this.products = value;
                this.OnPropertyChanged();
            }
        }

        public string Order
        {
            get => this.order;
            set
            {
                if (value == this.order) return;

                this.order = value;
                this.OnPropertyChanged();
            }
        }

        public string Delivery
        {
            get => this.delivery;
            set
            {
                if (value == this.delivery) return;

                this.delivery = value;
                this.OnPropertyChanged();
            }
        }

        public void SearchProdcts()
        {
            this.ecExchanger.Tell(new SearchProducts());
        }

        public void PlaceAnOrder()
        {
            this.ecExchanger.Tell(new OrderProducts());
        }

        public void PayForOrder()
        {
            this.ecExchanger.Tell(new PayForOrder());
        }

        public void Dispose()
        {
            this.ecExchanger.Tell(PoisonPill.Instance);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}