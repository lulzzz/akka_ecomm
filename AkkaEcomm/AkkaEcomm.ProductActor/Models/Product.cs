using System;

namespace AkkaEcomm.ProductActor.Models {
    public class Product {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
    }
}

