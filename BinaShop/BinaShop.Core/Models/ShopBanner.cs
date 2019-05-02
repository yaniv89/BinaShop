using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaShop.Core.Models
{
    public class ShopBanner : BaseEntity
    {
        public ShopBanner()
        {
        }
        [DisplayName("שם:")]
        public string Name { get; set; }
        [DisplayName("תמונה:")]
        public string Image { get; set; }
        [DisplayName("לינק:")]
        public string Link { get; set; }
    }
}
