﻿using System;
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
        [DisplayName("שם פרטי:")]
        public string FirstName { get; set; }
        [DisplayName("שם משפחה:")]
        public string LastName { get; set; }
        [DisplayName("דואר אלקטרוני:")]
        public string Email { get; set; }
        [DisplayName("רחוב:")]
        public string Street { get; set; }
        [DisplayName("עיר:")]
        public string City { get; set; }
        [DisplayName("מיקוד:")]
        public string ZipCode { get; set; }

    }
}
