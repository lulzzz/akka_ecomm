using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaEcomm.BasketActor.Events
{

    public class ItemAdded : BasketEventBase
    {
        public readonly Guid BasketItemId;

        public ItemAdded(Guid basketItemId = new Guid())
        {
            this.BasketItemId = basketItemId;
        }
    }

}
