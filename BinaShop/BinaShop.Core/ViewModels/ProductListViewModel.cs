using BinaShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaShop.Core.ViewModels
{
    public class ProductListViewModel
    {

        public IEnumerable<GalleryPic> GalleryPics { get; set; }
        public IEnumerable<ShopBanner> ShopBanners { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
