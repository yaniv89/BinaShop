using BinaShop.Core.Contracts;
using BinaShop.Core.Models;
using BinaShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BinaShop.WebUI.Controllers
{
    [RequireHttps]
    [Authorize(Roles = "Admin")]
    public class GalleryPicController : Controller
    {
        public IRepository<GalleryPic> GalleryPiccontext;

        public GalleryPicController(IRepository<GalleryPic> GalleryPicContext)
        {
            GalleryPiccontext = GalleryPicContext;

        }
        // GET: GalleryPic
        public ActionResult Index()
        {
            List<GalleryPic> galleryPics = GalleryPiccontext.Collection().ToList();



            return View(galleryPics);
        }
        //[HttpGet]
        //public ActionResult AddGallery()
        //{
        //    GalleryPicViewModel viewModel = new GalleryPicViewModel();
        //    viewModel.Photos = new List<Photo>();
        //    viewModel.GalleryPic = new GalleryPic();
        //    return View(viewModel);
        //}
        //[HttpPost]
        //public ActionResult AddGallery(GalleryPic galleryPic, IEnumerable<HttpPostedFileBase> files)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(galleryPic);
        //    }
        //    else
        //    {
        //        if (files != null)
        //        {

        //            string path = Server.MapPath("//Content//GalleryPics//" + galleryPic.Id);

        //            if (!Directory.Exists(path))
        //            {
        //                Directory.CreateDirectory(path);
        //            }
        //            galleryPic.Photos = new List<Photo>();
        //            foreach (HttpPostedFileBase photo in files)
        //            {
        //                if (photo != null && photo.ContentLength > 0)
        //                {
        //                    string ImageUrl = Path.GetFileName(photo.FileName);
        //                    Photo p = new Photo
        //                    {
        //                        ImageUrl = ImageUrl
        //                    };


        //                    photo.SaveAs(path + "//" + ImageUrl);
        //                    galleryPic.Photos.Add(p);
        //                }
        //            }
        //        }
        //        GalleryPiccontext.Insert(galleryPic);
        //        ViewBag.galleryPic = galleryPic;
        //        GalleryPiccontext.Commit();
        //        return RedirectToAction("Index");
        //    }
        //}
        public ActionResult Edit(string Id)
        {
            GalleryPic galleryPic = GalleryPiccontext.Find(Id);
            if (galleryPic == null)
            {
                return HttpNotFound();
            }
            else
            {
                GalleryPicViewModel viewModel = new GalleryPicViewModel();
                viewModel.GalleryPic = galleryPic;
                viewModel.Photos = galleryPic.Photos;
                ViewBag.galleryPic = galleryPic;
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(GalleryPic galleryPic, string Id, IEnumerable<HttpPostedFileBase> GalleryFiles)
        {
            GalleryPic galleryPicToEdit = GalleryPiccontext.Find(Id);

            if (galleryPicToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                if (!ModelState.IsValid)
                {
                    return View(galleryPic);
                }
                
                if (GalleryFiles != null)
                {
                    foreach (Photo p in galleryPicToEdit.Photos)
                    {
                        string path = Server.MapPath("//Content//GalleryPics//" + galleryPicToEdit.Id);
                        string DPath = path + "//" + p.ImageUrl;
                        System.IO.File.Delete(DPath);
                    }
                    galleryPicToEdit.Photos = new List<Photo>();
                    foreach (HttpPostedFileBase photo in GalleryFiles)
                    {
                        if (photo != null && photo.ContentLength > 0)
                        {
                            string ImageUrl = Path.GetFileName(photo.FileName);
                            Photo p = new Photo
                            {
                                ImageUrl = ImageUrl
                            };
                            WebImage img = new WebImage(photo.InputStream);
                            if (img.Width > 1122 && img.Height > 223)
                            {
                                img.Resize(1122, 223);
                            }
                            string savepath = Server.MapPath("//Content//GalleryPics//" + galleryPic.Id + "//") + ImageUrl;
                            img.Save(savepath, System.Drawing.Imaging.ImageFormat.Jpeg.ToString());
                            galleryPicToEdit.Photos.Add(p);
                            photo.InputStream.Dispose();
                            
                        }
                    }
                }
            }
            
                ViewBag.galleryPic = galleryPic;
            GalleryPiccontext.Commit();

                return RedirectToAction("Index");
            }
        }
}