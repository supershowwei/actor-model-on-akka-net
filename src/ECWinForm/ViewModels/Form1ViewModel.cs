using System.ComponentModel;
using System.Runtime.CompilerServices;
using Akka.Actor;
using ECLab.Model.Messages;
using ECWinForm.Annotations;
using ECWinForm.UIWorkers;

namespace ECWinForm.ViewModels
{
    public class Form1ViewModel : INotifyPropertyChanged
    {
        private readonly IActorRef exchanger;
        private string products;
        private string order;
        private string delivery;

        public Form1ViewModel()
        {
            this.exchanger = ECSystem.Instance.ActorOf(Form1Exchanger.Props(this).WithDispatcher("akka.actor.synchronized-dispatcher"));
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

        public void SearchProducts() => this.exchanger.Tell(new SearchProducts());

        public void PlaceAnOrder() => this.exchanger.Tell(new OrderProducts());

        public void PayForOrder() => this.exchanger.Tell(new PayForOrder());

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}