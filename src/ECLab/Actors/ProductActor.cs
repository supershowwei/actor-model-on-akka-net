using System;
using System.Collections.Generic;
using Akka;
using Akka.Actor;
using ECLab.Model.Data;
using ECLab.Model.Messages;

namespace ECLab.Actors
{
    public class ProductActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            message.Match().With<SearchProducts>(msg => this.HandleSearchProducts(msg));
        }

        private void HandleSearchProducts(SearchProducts cmd)
        {
            var products = this.Query(cmd.Keyword);
            
            this.Sender.Tell(new ProductsSearchResult { Products = products });
        }

        private List<Product> Query(string keyword)
        {
            return new()
                   {
                       new() { Id = 1, Name = "口罩", Price = 10 },
                       new() { Id = 2, Name = "酒精", Price = 20 },
                       new() { Id = 3, Name = "電影票", Price = 30 }
                   };
        }
    }
}