using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkkaEcomm.Common;

namespace AkkaEcomm.BasketActor.Messages
{
    public class AddItemToBasket : Customer
    {
        public readonly Guid ProductId;
        public readonly int Quantity;

        public AddItemToBasket(Guid customerId = default(Guid), Guid productId = default(Guid),
            int quantity = 0) : base(customerId)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }
}
