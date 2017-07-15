using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkkaEcomm.Common;

namespace AkkaEcomm.BasketActor.Messages
{
    public class RemoveItemFromBasket : Customer
    {
        public readonly Guid BasketItemId;

        public RemoveItemFromBasket(Guid customerId = default(Guid), Guid basketItemId = new Guid()) : base(customerId)
        {
            this.BasketItemId = basketItemId;
        }
    }
}
