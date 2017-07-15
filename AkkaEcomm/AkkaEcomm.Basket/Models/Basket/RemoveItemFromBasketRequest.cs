using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkkaEcomm.Basket.Models.Basket
{
    public class RemoveItemFromBasketRequest
    {
        public Guid CustomerId { get; set; }

        public Guid BasketId { get; set; }
    }
}