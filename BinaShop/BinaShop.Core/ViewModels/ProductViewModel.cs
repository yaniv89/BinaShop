using BinaShop.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BinaShop.Core.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            this.Photos = new List<Photo>();
        }
        public ProductViewModel(Photo photo)
        {
            ImageUrl = photo.ImageUrl;
            PhotoId = photo.Id;
        }
        public string ImageUrl { get; set; }
        public string PhotoId { get; set; }

        public ProductViewModel(Product row)
        {
            Id = row.Id;
            Name = row.Name;
            Description = row.Description;
            Price = row.Price;
            Category = row.Category;
            Image = row.Image;
            Photos = row.Photos;

        }

        public string Id { get; set; }
        [Required]
        [Display(Name ="שם המוצר: ")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "תיאור המוצר: ")]
        public string Description { get; set; }
        [Display(Name = "מחיר: ")]
        public decimal Price { get; set; }
        [Display(Name = "קטגוריה: ")]
        public string Category { get; set; }
        [Display(Name = "תמונת מוצר: ")]
        public string Image { get; set; }
        [Display(Name = "גלריית מוצר: ")]
        public List<Photo> Photos { get; set; }

        public Product Product { get; set; }
        public IEnumerable<ShopBanner> ShopBanners { get; set; }
    }
}

