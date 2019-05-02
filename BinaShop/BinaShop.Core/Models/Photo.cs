using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaShop.Core.Models
{
    public class Photo:BaseEntity
    {
        public string ImageUrl { get; set; }
        public virtual Product Product { get; set; }
        public virtual GalleryPic GalleryPic { get; set; }
    }
}
