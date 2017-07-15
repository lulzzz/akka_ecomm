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
    [RoutePrefix("api/basket")]
    public class BasketController : ApiController
    {
        private readonly BasketsActorProvider _basketActorProvider;

        public BasketController(BasketsActorProvider basketActorProvider)
        {
            _basketActorProvider = basketActorProvider;
        }
        
        [HttpPost]
        public async Task<IHttpActionResult> AddToBasket([FromBody] AddItemToBasketRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await this._basketActorProvider.Get().Ask<BasketEventBase>(new AddItemToBasket(
                    request.CustomerId,
                    request.ProductId,
                    request.Quantity
                ));

                if (result is ItemAdded)
                {
                    var e = result as ItemAdded;
                    return Created($"/api/baskets/{request.CustomerId}/", e.BasketItemId);
                }
                else if (result is ProductNotFound)
                {
                    return BadRequest("Product Not Found!");
                }
                else if (result is OutOfStock)
                {
                    return BadRequest("Out Of Stock!");
                }
                else
                {
                    return StatusCode(HttpStatusCode.SeeOther);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> RemoveFromBasket([FromBody] RemoveItemFromBasketRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await this._basketActorProvider.Get().Ask<BasketEventBase>(new RemoveItemFromBasket(
                    request.CustomerId,
                    request.BasketId
                ));

                if (result is ItemRemoved)
                {
                    return Ok();
                }
                else if (result is ItemNotFound)
                {
                    return BadRequest("Product Not Found!");
                }
                else
                {
                    return StatusCode(HttpStatusCode.SeeOther);
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
