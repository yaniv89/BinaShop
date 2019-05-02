using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BinaShop.Core.Models
{
    public class Product : BaseEntity
    {
        public Product() {
        }
        [StringLength(30)]
        [DisplayName("שם המוצר:")]
        [Required]
        public string Name { get; set; }

        [DisplayName("תיאור:")]
        [Required]
        public string Description { get; set; }

        [Range(0, 1500)]
        [Required]
        [DisplayName("מחיר:")]
        public decimal Price { get; set; }
        [DisplayName("קטגוריה:")]
        [Required]
        public string Category { get; set; }
        [DisplayName("תמונה:")]
        public string Image { get; set; }
        public string Currency { get; set; }
        [DisplayName("כמות:")]
        public int Quantity { get; set; }
        [DisplayName("גלריה:")]
        
        public virtual List<Photo> Photos { get; set; }
    }
}
