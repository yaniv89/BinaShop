using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaShop.Core.Models
{
    public class ProductCategory : BaseEntity
    {

        [DisplayName("קטגוריה:")]
        public string Category { get; set; }
        public int Sorting { get; set; }
    }
}
