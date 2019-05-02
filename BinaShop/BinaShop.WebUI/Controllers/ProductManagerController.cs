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
using BinaShop.DataAccess.InMemory;
using PagedList;

namespace BinaShop.WebUI.Controllers
{
    [RequireHttps]
    [Authorize(Roles = "Admin")]
    public class ProductManagerController : Controller
    {

        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        IRepository<Photo> photoContext;
        public ProductManagerController(IRepository<Photo> photoContexts, IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
            photoContext = photoContexts;
        }
        // GET: ProductManager
        public ActionResult Index(int? page)
        {
            List<Product> products = context.Collection().ToList();
            var pageNumber = page ?? 1;
            var onePageOfProducts = products.ToPagedList(pageNumber, 10);
            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            viewModel.Photos = new List<Photo>();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create( Product product,IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                HttpPostedFileBase file = Request.Files["mainPhoto"];
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    WebImage img = new WebImage(file.InputStream);
                    img.Resize(260, 260);
                    string savepath = Server.MapPath("//Content//ProductImages//") + product.Image;
                    img.Save(savepath,System.Drawing.Imaging.ImageFormat.Jpeg.ToString());
                    file.InputStream.Dispose();
                }

                if (files != null)
                {

                    string path = Server.MapPath("//Content//ProductsGallery//"+ product.Id);
                    
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    product.Photos = new List<Photo>();
                    foreach (HttpPostedFileBase photo in files)
                    {
                        if (photo != null && photo.ContentLength > 0)
                        {
                            string ImageUrl = photo.FileName + Path.GetFileName(photo.FileName);
                            Photo p = new Photo
                            {
                                ImageUrl = ImageUrl
                            };
                            WebImage img = new WebImage(photo.InputStream);
                            img.Resize(400, 400);
                            string savepath = path + "/" + ImageUrl;
                            img.Save(savepath, System.Drawing.Imaging.ImageFormat.Jpeg.ToString());
                            product.Photos.Add(p);
                            photo.InputStream.Dispose();
                        }
                    }
                }
                context.Insert(product);
                ViewBag.product = product;
                context.Commit();
                return RedirectToAction("Index");
            }

        }


        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                viewModel.Photos =product.Photos;
                ViewBag.product = product;
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit( Product product, string Id, HttpPostedFileBase file,IEnumerable<HttpPostedFileBase> photos)
        {
            Product productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                
               
                if (file != null)
                {
                    string imagepath = Server.MapPath("//Content//ProductImages//" + productToEdit.Image);
                    System.IO.File.Delete(imagepath);
                    WebImage img = new WebImage(file.InputStream);
                    img.Resize(260, 260);
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    string savepath = Server.MapPath("//Content//ProductImages//") + productToEdit.Image;
                    img.Save(savepath, System.Drawing.Imaging.ImageFormat.Jpeg.ToString());
                    file.InputStream.Dispose();
                }
                productToEdit.Photos = new List<Photo>();
                if (photos != null)
                {
                    
                    foreach (HttpPostedFileBase photo in photos)
                    {
                        if (photo != null && photo.ContentLength>0)
                        {
                            foreach (Photo ph in productToEdit.Photos)
                            {

                                string path = Server.MapPath("//Content//ProductsGallery//" + productToEdit.Id);
                                string DPath = path + "/" + ph.ImageUrl;
                                System.IO.File.Delete(DPath);

                            }
                            productToEdit.Photos.Clear();
                            string ImageUrl = photo.FileName + Path.GetFileName(photo.FileName);
                            Photo p = new Photo
                            {
                                ImageUrl = ImageUrl
                            };
                            WebImage img = new WebImage(photo.InputStream);
                            img.Resize(400, 400);
                            string savepath = Server.MapPath("~/Content/ProductsGallery/" + product.Id + "/") + ImageUrl;
                            img.Save(savepath, System.Drawing.Imaging.ImageFormat.Jpeg.ToString());
                            productToEdit.Photos.Add(p);
                            photo.InputStream.Dispose();
                        }
                    }
                }
                

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                ViewBag.product = product;
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach(Photo p in productToDelete.Photos)
                {
                    string path = Server.MapPath("//Content//ProductsGallery//" + productToDelete.Id);
                    string DPath = path +"/"+ p.ImageUrl;
                    System.IO.File.Delete(DPath);
                }
                string imagepath = Server.MapPath("//Content//ProductImages//" + productToDelete.Image);
                System.IO.File.Delete(imagepath);
                string Folderpath = Server.MapPath("//Content//ProductsGallery//" + productToDelete.Id);
                System.IO.Directory.Delete(Folderpath);
                productToDelete.Photos.RemoveRange(0,productToDelete.Photos.Count);
                context.Delete(Id);
                
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}