using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkkaEcomm.Basket.Models.Basket
{
    public class AddItemToBasketRequest
    {
        public Guid CustomerId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}