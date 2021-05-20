using System;
using System.Collections.Generic;
using System.Threading;
using Akka.Actor;

namespace DeadChild
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var actorSys = ActorSystem.Create("sys");

            var actor = actorSys.ActorOf(Props.Create<MyActor>());

            for (var i = 0; i < 10; i++)
            {
                actor.Tell(i);
            }

            Console.Read();
        }
    }

    internal class MyActor : UntypedActor
    {
        private readonly List<object> messages = new();

        public MyActor()
        {
            Console.WriteLine("create");
        }
         
        protected override void PreRestart(Exception reason, object message)
        {
            this.Self.Tell(message);

            base.PreRestart(reason, message);
        }

        protected override void OnReceive(object message)
        {
            if (message.Equals(5)) throw new Exception("ex");

            this.messages.Add(message);
            Console.WriteLine($"Msg:{message}, Cnt:{this.messages.Count}");

            Thread.Sleep(1000);
        }
    }
}
