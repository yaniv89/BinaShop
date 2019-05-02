using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BinaShop.Core.Contracts;
using BinaShop.Core.Models;
using BinaShop.Core.ViewModels;




namespace BinaShop.WebUI.Controllers
{
    [RequireHttps]
    [Authorize(Roles = "Admin")]
    public class ShopBannerController : Controller
    {
        public IRepository<ShopBanner> Bannercontext;

        public ShopBannerController(IRepository<ShopBanner> ShopBannerContext)
        {
            Bannercontext = ShopBannerContext;

        }
        // GET: Pages
        public ActionResult Index()
        {
            
            //Declare list
            List<ShopBanner> shopBanners = Bannercontext.Collection().ToList();



            return View(shopBanners);
        }
        //Get: Admin/Pages/AddBanner
        [HttpGet]
        public ActionResult AddBanner()
        {
            ShopBannerViewModel viewModel = new ShopBannerViewModel();

            viewModel.ShopBanner = new ShopBanner();
            return View(viewModel);
        }
        //Post: Admin/Pages/AddBanner
        [HttpPost]
        public ActionResult AddBanner(ShopBanner shopBanner)
        {

            //check model state
            if (!ModelState.IsValid)
            {
                return View(shopBanner);
            }

            else
            {
                HttpPostedFileBase file = Request.Files["BannerP"];
                if (file != null)
                {
                    shopBanner.Image = shopBanner.Id + Path.GetExtension(file.FileName);
                    WebImage img = new WebImage(file.InputStream);
                    img.Resize(350, 520);
                    string savepath = Server.MapPath("//Content//Banners//") + shopBanner.Image;
                    img.Save(savepath, System.Drawing.Imaging.ImageFormat.Jpeg.ToString());
                    file.InputStream.Dispose();
                }

                Bannercontext.Insert(shopBanner);
                ViewBag.shopBanner = shopBanner;
                Bannercontext.Commit();
                return RedirectToAction("Index");
            }

        }

        //Get: Admin/Pages/EditBanner/id
        [HttpGet]
        public ActionResult EditBanner(string Id)
        {
            ShopBanner shopBanner = Bannercontext.Find(Id);
            if (shopBanner == null)
            {
                return HttpNotFound();
            }
            else
            {
                ShopBannerViewModel viewModel = new ShopBannerViewModel();
                viewModel.ShopBanner = shopBanner;

                ViewBag.shopBanner = shopBanner;
                return View(viewModel);
            }

        }
        //Post: Admin/Pages/EditPage/id
        [HttpPost]
        public ActionResult EditBanner(ShopBanner shopBanner, string Id)
        {
            ShopBanner shopBannerToEdit = Bannercontext.Find(Id);
            if (shopBannerToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(shopBanner);
                }
                
                HttpPostedFileBase file = Request.Files["Banner"];
                if (file != null)
                {
                    string imagepath = Server.MapPath("//Content//Banners//" + shopBannerToEdit.Image);
                    System.IO.File.Delete(imagepath);
                    WebImage img = new WebImage(file.InputStream);
                    if (shopBannerToEdit.Id == "b6248a24-757a-4505-948e-f5028f73ab34")
                    {
                        img.Resize(350, 520);
                    }
                    else if (shopBannerToEdit.Id == "cb0da0c6-401d-404c-abd7-e7bb5e972109" && img.Width>1920 &&img.Height>1080 )
                    {
                        img.Resize(1920, 1080);
                    }
                    else if(shopBannerToEdit.Id== "7379d3f0-8222-4725-852b-59b46f83aa67" || shopBannerToEdit.Id == "cf7eaba3-f78c-4208-b26e-a0bb96672209" || shopBannerToEdit.Id == "feaf1d5a-4164-4bbd-a404-a89e0dd963a3") {
                        img.Resize(150, 600);
                    }
                    
                        string savepath = Server.MapPath("//Content//Banners//") + shopBannerToEdit.Image;
                    img.Save(savepath, System.Drawing.Imaging.ImageFormat.Jpeg.ToString());
                    shopBannerToEdit.Image = shopBannerToEdit.Id + Path.GetExtension(img.FileName);
                    file.InputStream.Dispose();
                }

                shopBannerToEdit.Name = shopBanner.Name;
                shopBannerToEdit.Link = shopBanner.Link;
                Bannercontext.Commit();
                TempData["AB"] = "ערכת את הבאנר בהצלחה.";
                return RedirectToAction("Index");
            }
        }


    }
}