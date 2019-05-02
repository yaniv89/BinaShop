using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaShop.Core.Models
{
    public class Basket: BaseEntity
    {
        public virtual List<BasketItem> BasketItems { get; set; }

        public Basket()
        {
            this.BasketItems = new List<BasketItem>();
        }
    }
}
