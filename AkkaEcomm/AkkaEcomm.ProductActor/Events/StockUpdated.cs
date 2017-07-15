using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkkaEcomm.ProductActor.Models;

namespace AkkaEcomm.ProductActor.Events
{

    public class StockUpdated : ProductEventBase
    {
        public readonly Product Product;

        public StockUpdated(Product product)
        {
            this.Product = product;
        }
    }
}
