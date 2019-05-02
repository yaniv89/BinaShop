using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaShop.Core.Models
{
    public class GalleryPic :   BaseEntity
    {
        [DisplayName("גלריה:")]

        public virtual List<Photo> Photos { get; set; }
    }
}
