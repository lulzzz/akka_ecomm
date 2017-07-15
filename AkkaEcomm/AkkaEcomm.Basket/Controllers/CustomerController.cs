using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Akka.Actor;
using AkkaEcomm.Basket.Models.Basket;
using AkkaEcomm.BasketActor;
using AkkaEcomm.BasketActor.Events;
using AkkaEcomm.BasketActor.Messages;

namespace AkkaEcomm.Basket.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        private readonly BasketsActorProvider _basketActorProvider;

        public CustomerController(BasketsActorProvider basketActorProvider)
        {
            _basketActorProvider = basketActorProvider;
        }
        
        [HttpGet]
        [Route("{customerId}/basket")]
        public async Task<BasketActor.Models.Basket> GetBasket(Guid customerId)
        {
            return await this._basketActorProvider.Get().Ask<BasketActor.Models.Basket>(new GetBasket(customerId));
        }
    }
}
