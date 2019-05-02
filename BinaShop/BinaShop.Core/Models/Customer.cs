using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaShop.Core.Models
{
    public class Customer : BaseEntity
    {
        public string UserId { get; set; }
        
        [DisplayName("דואר אלקטרוני:")]
        public string Email { get; set; }
        

    }
}
