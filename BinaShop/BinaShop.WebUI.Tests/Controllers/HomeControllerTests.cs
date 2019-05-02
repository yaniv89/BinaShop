using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinaShop.WebUI;
using BinaShop.WebUI.Controllers;
using BinaShop.Core.Contracts;
using BinaShop.Core.Models;
using BinaShop.Core.ViewModels;

namespace BinaShop.WebUI.Tests.Controllers
{
    //[TestClass]
    //public class UnitTest1
    //{
    //    [TestMethod]
    //    public void ProductPageDoesReturnProducts()
    //    {
    //        IRepository<Product> productContext = new Mocks.MockContext<Product>();
    //        IRepository<ProductCategory> productCategoryContext = new Mocks.MockContext<ProductCategory>();

    //        productContext.Insert(new Product());

    //        HomeController controller = new HomeController(productContext, productCategoryContext);

    //        var result = controller.Products() as ViewResult;
    //        var viewModel = (ProductListViewModel) result.ViewData.Model;

    //        Assert.AreEqual(1, viewModel.Products.Count());
    //    }
    //}
}
