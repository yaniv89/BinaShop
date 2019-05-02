using BinaShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaShop.DataAccess.SQL
{
    public class DataContext:DbContext
    {
        public DataContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShopBanner> ShopBanners { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PriceOffer> PriceOffers { get; set; }
        public DbSet<GalleryPic> GalleryPics { get; set; }
    }
}
