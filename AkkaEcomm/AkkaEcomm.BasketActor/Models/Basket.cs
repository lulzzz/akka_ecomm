using System.Collections.Generic;

namespace AkkaEcomm.BasketActor.Models {
    public class Basket {

        public List<BasketItem> Products { get; set; } = new List<BasketItem>();
    }
}
