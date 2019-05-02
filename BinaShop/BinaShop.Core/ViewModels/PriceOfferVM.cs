using BinaShop.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BinaShop.Core.ViewModels
{
    public class PriceOfferVM
    {
        public PriceOfferVM()
        {

        }
    

        public PriceOfferVM(PriceOffer priceOffer)
        {
            FromName = priceOffer.FromName;
            FromEmail = priceOffer.FromEmail;
            Date = priceOffer.Date;
            Sadnas = priceOffer.Sadnas;
            NumOfPpl = priceOffer.NumOfPpl;
            Message = priceOffer.Message;
            PhoneNum = priceOffer.PhoneNum;
        }
        [Required, Display(Name = "שם")]
        public string FromName { get; set; }
        [Required, Display(Name = "דואר אלקטרוני"), EmailAddress]
        public string FromEmail { get; set; }
        [Required]
        [Display(Name = "טלפון:")]
        public string PhoneNum { get; set; }
        [Required]
        [Display(Name = "בחר תאריך: ")]
        public string Date { get; set; }
        [Display(Name = "בחר פעילות: ")]
        [Required]
        public string Sadnas { get; set; }
        [Display(Name = "בחר מספר משתתפים: ")]
        public int NumOfPpl { get; set; }
        [Required, Display(Name = "הקלד הודעה: ")]
        [AllowHtml]
        public string Message { get; set; }
        public PriceOffer PriceOffer { get; set; }
    }
}
