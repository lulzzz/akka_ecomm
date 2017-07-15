using System;

namespace AkkaEcomm.BasketActor.Models {
    public class BasketItem {
        public Guid Id { get; set; }

        public Guid ProductId { get ; set; }

        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        
    }
}
