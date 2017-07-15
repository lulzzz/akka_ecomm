using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Akka.Actor;
using AkkaEcomm.Basket.Controllers;
using AkkaEcomm.BasketActor;
using AkkaEcomm.ProductActor;
using Autofac;
using Autofac.Integration.WebApi;

namespace AkkaEcomm.Basket
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<BasketController>().InstancePerRequest();
            builder.RegisterType<CustomerController>().InstancePerRequest();

            ActorSystem actorSystem = ActorSystem.Create("akkaecommbasket");
            ProductsActorProvider productsActorProvider = new ProductsActorProvider(actorSystem);

            builder.Register(context => actorSystem).As<ActorSystem>().SingleInstance();

            builder.Register(context => productsActorProvider).As<ProductsActorProvider>().SingleInstance();
            builder.Register(context => new BasketsActorProvider(actorSystem, productsActorProvider))
                .As<BasketsActorProvider>().SingleInstance();

            builder.RegisterWebApiFilterProvider(config);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
