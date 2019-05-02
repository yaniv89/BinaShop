using BinaShop.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BinaShop.Core.ViewModels
{
    public class ShopBannerViewModel
    {
        public ShopBannerViewModel()
        {

        }
        public ShopBannerViewModel(ShopBanner row)
        {
            Name = row.Name;
            Image = row.Image;
            Link = row.Link;
        }
        [DisplayName("שם:")]
        [Required]
        public string Name { get; set; }
        [DisplayName("תמונה:")]
        public string Image { get; set; }
        [DisplayName("לינק:")]
        public string Link { get; set; }
        public ShopBanner ShopBanner { get; set; }
        public IEnumerable<ShopBanner> ShopBanners { get; set; }
    }
}
