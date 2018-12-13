using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaShop.Core.Models
{
    public class Product
    {
        public string Id { get; set; }
        
        [StringLength(30)]
        [DisplayName("שם המוצר")]
        public string Name { get; set; }

        [DisplayName("תיאור")]
        public string Description { get; set; }

        [Range(0, 1500)]
        [DisplayName("מחיר")]
        public decimal Price { get; set; }
        [DisplayName("קטגוריה")]
        public string Category { get; set; }
        [DisplayName("תמונה")]
        public string Image { get; set; }

        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
