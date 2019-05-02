using BinaShop.Core.Contracts;
using BinaShop.Core.Models;
using BinaShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PayPal;
using PagedList;
using System.IO;
using BinaShop.DataAccess.SQL;

namespace BinaShop.WebUI.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        IRepository<PriceOffer> priceOfferContext;
        public IRepository<ShopBanner> Bannercontext;
        public IRepository<GalleryPic> GalleryPiccontext;
        public HomeController(IRepository<GalleryPic> GalleryPicContext, IRepository<ShopBanner> Bannercontextt,IRepository<PriceOffer> priceOfferContextr, IRepository<Product> productContext, IRepository<ProductCategory> productCategotyContext)
        {
            Bannercontext = Bannercontextt;
            context = productContext;
            productCategories = productCategotyContext;
            priceOfferContext = priceOfferContextr;
            GalleryPiccontext = GalleryPicContext;
        }
        public ActionResult Index()
        {
            List<ShopBanner> shopBanners = Bannercontext.Collection().ToList();
            ShopBannerViewModel model = new ShopBannerViewModel();
            model.ShopBanners = shopBanners;
            return View(model);
        }

        public ActionResult Sadna()
        {
            PriceOfferVM model = new PriceOfferVM();
            model.PriceOffer = new PriceOffer();
            List<int> NewList = new List<int>() {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15 };
            
            ViewBag.NewList = NewList.ToList();
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Sadna(PriceOffer model)
        {

            
               

            if (ModelState.IsValid)
            {
                
                var body = "<p dir=rtl>" +
                    "שם: {0}<br/>" +
                    "דואר אלקטרוני: {1}<br/>" +
                    "טלפון: {2}<br/>" +
                    "תאריך: {3}<br/>" +
                    "מספר משתתפים: {4}<br/>" +
                    "סדנא: {5}<br/>" +
                     "הודעה: {6}</p>";
                
                var message = new MailMessage();
                message.To.Add(new MailAddress("postmaster@halohdim.com"));  // replace with valid value 
                message.From = new MailAddress("postmaster@halohdim.com", "הלוכדים של בינה");  // replace with valid value
                message.Subject = "סדנאות ";
                message.Body = string.Format(body, model.FromName, model.FromEmail,model.PhoneNum,model.Date,model.NumOfPpl,model.Sadnas, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "postmaster@halohdim.com",  // replace with valid value
                        Password = "******"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "mail.halohdim.com";
                    smtp.Port = 8889;
                    smtp.EnableSsl = false;
                    await smtp.SendMailAsync(message);
                    TempData["SM"] = "הודעתך נשלחה בהצלחה, אשיב בהקדם.";
                    return RedirectToAction("Sadna");
                }
            }
            return View(model);
        }

        public ActionResult Contact()
        {
            return View();
        }
        
        public ActionResult Products(int? page,string Category = null)
        {
            List<Product> products;
            List<ProductCategory> categories = productCategories.Collection().ToList();
            List<ShopBanner> shopBanners = Bannercontext.Collection().ToList();
            List<GalleryPic> galleryPics = GalleryPiccontext.Collection().ToList();
            if (Category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Category == Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = categories;
            model.ShopBanners = shopBanners;
            model.GalleryPics = galleryPics;
            var pageNumber = page ?? 1;
            var onePageOfProducts = products.ToPagedList(pageNumber, 12);
            ViewBag.OnePageOfProducts = onePageOfProducts;

            return View(model);
        }

        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                List<ShopBanner> shopBanners = Bannercontext.Collection().ToList();
                IEnumerable<Photo> photos = product.Photos;
                ProductViewModel viewModel = new ProductViewModel() {
                    Product = product,
                    Category = product.Category,
                    Description = product.Description,
                    Price = product.Price,
                    Image = product.Image,
                    Name = product.Name,
                    Photos = photos.Select(x => new Photo() { ImageUrl = x.ImageUrl, Id = x.Id }).ToList(),
                    ShopBanners = shopBanners
            };
               

                ViewBag.product = product;
                return View(viewModel);
                
                
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModels model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>דואר אלקטרוני מ: {0} ({1})</p><p>הודעה:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("postmaster@halohdim.com"));  // replace with valid value 
                message.From = new MailAddress("postmaster@halohdim.com");  // replace with valid value
                message.Subject = "יצירת קשר ";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "postmaster@halohdim.com",  // replace with valid value
                        Password = "*****"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "mail.halohdim.com";
                    smtp.Port = 8889;
                    smtp.EnableSsl = false;
                    await smtp.SendMailAsync(message);
                    TempData["SM"] = "הודעתך נשלחה בהצלחה, אשיב בהקדם.";
                    return RedirectToAction("Contact");
                }
            }
            return View(model);
        }
        public ActionResult Sent()
        {
            return View();
        }
    }
}