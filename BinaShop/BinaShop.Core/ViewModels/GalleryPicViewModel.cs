using BinaShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaShop.Core.ViewModels
{
    public class GalleryPicViewModel
    {
        public GalleryPicViewModel()
        {

        }
        public GalleryPicViewModel(Photo photo)
        {
            ImageUrl = photo.ImageUrl;
            PhotoId = photo.Id;
        }
        public string ImageUrl { get; set; }
        public string PhotoId { get; set; }
        public GalleryPicViewModel(GalleryPic row)
        {
            Id = row.Id;
            Photos = row.Photos;
        }
        public List<Photo> Photos { get; set; }
        public string Id { get; set; }

        public GalleryPic GalleryPic { get; set; }
    }
}

