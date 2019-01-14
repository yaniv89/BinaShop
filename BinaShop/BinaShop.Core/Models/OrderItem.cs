using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaShop.Core.Models
{
    public class OrderItem : BaseEntity
    {
        [DisplayName("מספר הזמנה:")]
        public string OrderId{  get; set; }
        public string ProductId { get; set; }
        [DisplayName("שם המוצר:")]
        public string ProductName { get; set; }
        [DisplayName("מחיר:")]
        public decimal Price { get; set; }
        [DisplayName("תמונה:")]
        public string Image { get; set; }
        [DisplayName("כמות:")]
        public int Quantity { get; set; }
    }
}
