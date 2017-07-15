using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Akka.Actor;
using AkkaEcomm.ProductActor.Events;
using AkkaEcomm.ProductActor.Messages;
using AkkaEcomm.ProductActor.Models;

namespace AkkaEcomm.ProductActor
{
    public partial class ProductsActor : ReceiveActor
    {
        private IList<Product> Products { get; }
        public ProductsActor(IList<Product> products)
        {
            this.Products = products;

            Receive<GetAllProducts>(_ => Sender.Tell(new ReadOnlyCollection<Product>(this.Products)));
            Receive<UpdateStock>(m => Sender.Tell(UpdateStockAction(m)));
        }

        public ProductEventBase UpdateStockAction(UpdateStock message)
        {
            var product = this.Products
                .FirstOrDefault(p => p.Id == message.ProductId);

            if (product == null) return new ProductNotFound();
            if (product.Stock + message.AmountChanged >= 0)
            {
                product.Stock += message.AmountChanged;
                return new StockUpdated(product);
            }
            else
            {
                return new OutOfStock();
            }
        }
    }
}
