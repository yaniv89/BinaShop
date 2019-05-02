using BinaShop.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BinaShop.Core.ViewModels
{
    public class ProductManagerViewModel
    {
        public ProductManagerViewModel()
        {
        }
        public ProductManagerViewModel(Photo photo)
        {
            ImageUrl = photo.ImageUrl;
            PhotoId = photo.Id;
        }
        public string ImageUrl { get; set; }
        public string PhotoId { get; set; }

        
        public ProductManagerViewModel(Product row)
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
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public List<Photo> Photos { get; set; }
        
        public Product Product { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
