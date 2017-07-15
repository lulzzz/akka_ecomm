using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaEcomm.Common
{
    public abstract class Customer
    {
        public readonly Guid CustomerId;

        protected Customer(Guid customerId = default(Guid))
        {
            this.CustomerId = customerId;
        }
    }
}
