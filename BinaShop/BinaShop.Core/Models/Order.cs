using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaShop.Core.Models
{
    public class Order:BaseEntity
    {
        
        [DisplayName("שם פרטי:")]
        public string FirstName { get; set; }
        [DisplayName("שם משפחה:")]
        public string LastName { get; set; }
        [DisplayName("דואר אלקטרוני:")]
        public string Email { get; set; }
        [DisplayName("רחוב:")]
        public string Street { get; set; }
        [DisplayName("עיר:")]
        public string City { get; set; }
        [DisplayName("מיקוד:")]
        public string ZipCode { get; set; }
        [DisplayName("סטטוס:")]
        public string OrderStatus { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        [DisplayName("סכום:")]
        public int Total { get; set; }
        public int Tax { get; set; }
        public int Subtotal { get; set; }
        public int Shipping { get; set; }
        [DisplayName("PayPal:")]
        public string PayPalReference { get; set; }
        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }
    }
}
