using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaShop.Core.Models
{


    public class EmailFormModels
    {
        [Required, Display(Name = "שם")]
        public string FromName { get; set; }
        [Required, Display(Name = "דואר אלקטרוני"), EmailAddress]
        public string FromEmail { get; set; }
        [Required, Display(Name ="הקלד הודעה")]
        public string Message { get; set; }
    }
}
