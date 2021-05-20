using ECLab.Model.Messages;
using ECSignalR.Messages;
using Microsoft.AspNetCore.SignalR;

namespace ECSignalR
{
    public class ECHub : Hub
    {
        public void SearchProducts()
        {
            ECSystem.Instance.ActorSelection("akka://ec/user/signalr-exchanger")
                .Tell(new SignalRMessage { Message = new SearchProducts(), Caller = this.Clients.Caller });
        }

        public void PlaceAnOrder()
        {
            ECSystem.Instance.ActorSelection("akka://ec/user/signalr-exchanger")
                .Tell(new SignalRMessage { Message = new OrderProducts(), Caller = this.Clients.Caller });
        }

        public void PayForOrder()
        {
            ECSystem.Instance.ActorSelection("akka://ec/user/signalr-exchanger")
                .Tell(new SignalRMessage { Message = new PayForOrder(), Caller = this.Clients.Caller });
        }
    }
}