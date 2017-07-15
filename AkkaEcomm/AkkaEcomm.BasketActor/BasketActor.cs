using System;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaEcomm.BasketActor.Events;
using AkkaEcomm.BasketActor.Messages;
using AkkaEcomm.BasketActor.Models;
using AkkaEcomm.ProductActor.Events;
using AkkaEcomm.ProductActor.Messages;
using AkkaEcomm.ProductActor.Models;
using ProductNotFound = AkkaEcomm.BasketActor.Events.ProductNotFound;

namespace AkkaEcomm.BasketActor
{
    public partial class BasketActor : ReceiveActor
    {
        public Basket BasketState { get; set; }
        private IActorRef ProductsActorRef { get; set; }
        public BasketActor(IActorRef productsActor)
        {
            this.BasketState = new Basket();
            this.ProductsActorRef = productsActor;

            Receive<GetBasket>(_ => Sender.Tell(this.BasketState));
            ReceiveAsync<AddItemToBasket>(m => AddItemToBasketAction(m).PipeTo(Sender), m => m.Quantity > 0);
            Receive<RemoveItemFromBasket>(m => Sender.Tell(RemoveItemToBasketAction(m)));
        }

        public static Props Props(IActorRef productsActor)
        {
            return Akka.Actor.Props.Create(() => new BasketActor(productsActor));
        }

        public async Task<BasketEventBase> AddItemToBasketAction(AddItemToBasket message)
        {
            var productActorResult = await this.ProductsActorRef.Ask<ProductEventBase>(
                new UpdateStock(
                    productId: message.ProductId,
                    amountChanged: -message.Quantity
                )
            );

            if (productActorResult is StockUpdated)
            {
                var product = ((StockUpdated)productActorResult).Product;
                return AddToBasket(product, message.Quantity) as ItemAdded;
            }
            else if (productActorResult is ProductActor.Events.ProductNotFound)
            {
                return new ProductNotFound();
            }
            else if (productActorResult is InsuffientStock)
            {
                return new OutOfStock();
            }
            else
            {
                throw new NotImplementedException($"Unknown response: {productActorResult.GetType().ToString()}");
            }
        }

        public BasketEventBase RemoveItemToBasketAction(RemoveItemFromBasket message)
        {
            var basketItem = this.BasketState.Products.Find(item => item.Id == message.BasketItemId);
            if (basketItem is BasketItem)
            {
                this.BasketState.Products.Remove(basketItem);
                return new ItemRemoved();
            }
            else
            {
                return new ItemNotFound();
            }
        }

        private ItemAdded AddToBasket(Product productToAdd, int quantity)
        {
            var existingBasketItemWithProduct = this.BasketState.Products.Find(item => item.ProductId == productToAdd.Id);
            if (existingBasketItemWithProduct != null)
            {
                existingBasketItemWithProduct.Quantity += quantity;
                return new ItemAdded(
                    basketItemId: existingBasketItemWithProduct.Id
                );
            }
            else
            {
                var basketItemId = Guid.NewGuid();
                this.BasketState.Products.Add(new BasketItem
                {
                    Id = basketItemId,
                    ProductId = productToAdd.Id,
                    Title = productToAdd.Title,
                    UnitPrice = productToAdd.UnitPrice,
                    Quantity = quantity
                });

                return new ItemAdded(basketItemId);
            }
        }
    }
}
