using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkkaEcomm.Common;

namespace AkkaEcomm.BasketActor.Messages
{

    public class GetBasket : Customer
    {
        public GetBasket(Guid customerId = default(Guid)) : base(customerId) { }
    }
}
