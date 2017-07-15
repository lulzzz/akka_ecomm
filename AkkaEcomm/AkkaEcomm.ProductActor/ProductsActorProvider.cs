using System.Collections.Generic;
using Akka.Actor;
using AkkaEcomm.ProductActor.Models;
using AkkaEcomm.ProductActor.Properties;
using Newtonsoft.Json;

namespace AkkaEcomm.ProductActor
{
    public class ProductsActorProvider
    {
        private IActorRef ProductsActor { get; set; }

        public ProductsActorProvider(ActorSystem actorSystem)
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(Resources.Products);   
            this.ProductsActor = actorSystem.ActorOf(Props.Create<AkkaEcomm.ProductActor.ProductsActor>(products), "products");
        }

        public IActorRef Get()
        {
            return ProductsActor;
        }
    }
}
