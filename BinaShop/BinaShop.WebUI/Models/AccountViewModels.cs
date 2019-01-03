using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BinaShop.WebUI.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "דואר אלקטרוני:")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "זכור דפדפן זה?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "דואר אלקטרוני:")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "דואר אלקטרוני:")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמה:")]
        public string Password { get; set; }

        [Display(Name = "זכור אותי?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "דואר אלקטרוני:")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "הסיסמה חייבת לכלול לפחות 6 תווים", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמה:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "אשר סיסמה:")]
        [Compare("Password", ErrorMessage = "הסיסמה ואישור הסיסמה אינם תואמים!")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DisplayName("שם פרטי:")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("שם משפחה:")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("רחוב:")]
        public string Street { get; set; }
        [Required]
        [DisplayName("עיר:")]
        public string City { get; set; }
        [Required]
        [DisplayName("מיקוד:")]
        public string ZipCode { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "דואר אלקטרוני:")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "הסיסמה חייבת לכלול 6 תווים לפחות.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "אשר סיסמה:")]
        [Compare("Password", ErrorMessage = "הסיסמה ואישור הסיסמה אינם תואמים!")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "דואר אלקטרוני:")]
        public string Email { get; set; }
    }
}
