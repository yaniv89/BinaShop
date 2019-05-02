using BinaShop.Core.Contracts;
using BinaShop.Core.Models;
using BinaShop.Core.ViewModels;
using BinaShop.DataAccess.SQL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BinaShop.WebUI.Controllers
{
    [RequireHttps]
    [Authorize(Roles = "Admin")]
    public class OrderManagerController : Controller
    {
        IOrderService orderService;
        IRepository<Order> context;
        public OrderManagerController(IOrderService OrderService, IRepository<Order> contextO)
        {
            this.orderService = OrderService;
            this.context = contextO;
        }
        // GET: OrderManager
        public ActionResult Index(int? page)
        {
            List<Order> orders = orderService.GetOrderList();
            var pageNumber = page ?? 1;
            var OnePageOfOrders = orders.OrderByDescending(x => x.CreatedAt).ToPagedList(pageNumber, 10);
            ViewBag.OnePageOfOrders = OnePageOfOrders;
            return View(orders);
        }

        public ActionResult UpdateOrder(string Id)
        {
            CultureInfo il = CultureInfo.GetCultureInfo("he-IL");
            ViewBag.il = il;
            ViewBag.StatusList = new List<string>()
            {
                "הזמנה נוצרה",
                "תשלום בוצע",
                "הזמנה נשלחה",
                "הזמנה הושלמה"
            };
            Order order = orderService.GetOrder(Id);
            return View(order);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateOrder(Order updatedorder,string Id)
        {
            Order order = orderService.GetOrder(Id);

            order.OrderStatus = updatedorder.OrderStatus;
            orderService.UpdateOrder(order);
            if(order.OrderStatus== "הזמנה נשלחה")
            {
                var body = "שלום, הזמנתך שמספרה:"+"<br/>" + order.Id + "<br/>" + "יצאה למשלוח, תודה שקניתם הלוכדים של בינה.";
                var message = new MailMessage();
                message.To.Add(new MailAddress(User.Identity.Name));  // replace with valid value 
                message.From = new MailAddress("postmaster@halohdim.com", "הלוכדים של בינה");  // replace with valid value
                message.Subject = "הזמנה מס'- " + order.Id;
                message.Body = string.Format(body);
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
                }
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult Delete(string Id)
        {
            Order orderToDelete = orderService.GetOrder(Id);
            if (orderToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(orderToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            DataContext db = new DataContext();
            Order orderToDelete = db.Orders.Find(Id);
            
            if (orderToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                
                db.OrderItems.RemoveRange(orderToDelete.OrderItems).Where(x => x.OrderId == Id);
                db.Orders.Remove(orderToDelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}