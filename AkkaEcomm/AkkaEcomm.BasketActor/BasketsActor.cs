using Akka.Actor;
using AkkaEcomm.Common;

namespace AkkaEcomm.BasketActor
{
    public class BasketsActor : ReceiveActor
    {
        private IActorRef ProductActor { get; }

        public BasketsActor(IActorRef productActor)
        {
            this.ProductActor = productActor;

            ReceiveAny(m =>
            {
                var customer = m as Customer;
                if (customer != null)
                {
                    var envelope = customer;
                    var basketActor = Context.Child(envelope.CustomerId.ToString()) is Nobody ?
                        Context.ActorOf(BasketActor.Props(this.ProductActor), envelope.CustomerId.ToString()) :
                        Context.Child(envelope.CustomerId.ToString());
                    basketActor.Forward(customer);
                }
            });
        }
        public static Props Props(IActorRef productsActor)
        {
            return Akka.Actor.Props.Create(() => new BasketsActor(productsActor));
        }
    }
}
