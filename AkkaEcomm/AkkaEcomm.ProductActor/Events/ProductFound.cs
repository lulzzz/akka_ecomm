using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkkaEcomm.ProductActor.Models;

namespace AkkaEcomm.ProductActor.Events
{
    public class ProductFound : ProductEventBase
    {
        public readonly Product Product;

        public ProductFound(Product product)
        {
            this.Product = product;
        }
    }
}
