using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaEcomm.ProductActor.Messages
{
    public class UpdateStock
    {
        public readonly Guid ProductId;
        public readonly int AmountChanged;

        public UpdateStock(Guid productId = default(Guid), int amountChanged = 0)
        {
            this.ProductId = productId;
            this.AmountChanged = amountChanged;
        }
    }
}
