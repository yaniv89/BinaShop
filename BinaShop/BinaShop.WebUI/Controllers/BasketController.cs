using BinaShop.Core.Contracts;
using BinaShop.Core.Models;
using BinaShop.Core.ViewModels;
using BinaShop.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayPal.Api;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net;
using System.Globalization;

namespace BinaShop.WebUI.Controllers
{
    [RequireHttps]
    public class BasketController : Controller
    {
        

        public IRepository<Customer> customers;
        IRepository<BinaShop.Core.Models.Order> orders;
        IBasketService basketService;
        public IOrderService orderService;

        public BasketController(IRepository<BinaShop.Core.Models.Order> Orders, IBasketService BasketService, IOrderService OrderService, IRepository<Customer> Customers)
        {
            this.basketService = BasketService;
            this.orderService = OrderService;
            this.customers = Customers;
            this.orders = Orders;
        }
        // GET: Basket
        public ActionResult Index()
        {
            CultureInfo il = CultureInfo.GetCultureInfo("he-IL");
            ViewBag.il = il;
            var model =  basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);
            TempData["SM"] = "המוצר התווסף לסל בהצלחה.";
            return Redirect("/Home/Products");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            CultureInfo il = CultureInfo.GetCultureInfo("he-IL");
            ViewBag.il = il;
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummary);
        }
        
        
       [Authorize]
        public ActionResult ThankYou(string OrderId)
        {

            ViewBag.OrderId = OrderId;
            return View();
        }

        [Authorize]
        public ActionResult Cancel()
        {
            TempData["SM"] = "התשלום נכשל, אנא נסה שנית.";
            return View();
        }

        [Authorize]
        public ActionResult Checkout()
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);


            var cart = basketService.GetBasketItems(this.HttpContext);
            if (cart.Any())
            {
                int shipping = 0;
                var taxrate = 0M;
                var subtotal = cart.Sum(x => x.Quantity * x.Price);
                var tax = Convert.ToInt32((subtotal + shipping) * taxrate);
                var total = subtotal + shipping + tax;
                var order = new BinaShop.Core.Models.Order()
                {
                    OrderStatus = "הזמנה נוצרה.",
                    Email = User.Identity.Name,
                    Subtotal =Convert.ToInt32(subtotal),
                    Tax = tax,
                    Shipping = shipping,
                    Total = Convert.ToInt32(total),
                    OrderItems = basketItems.Select(x => new OrderItem()
                    {
                        ProductName = x.ProductName,
                        Price = x.Price,
                        Quantity = x.Quantity
                    }).ToList()
                };
                orders.Insert(order);
                orders.Commit();


                var apiContext = PayPalConfiguration.GetAPIContext();

                // Create a new payment object
                var payment = new Payment
                {
                    intent = "sale",
                    payer = new Payer
                    {
                        payment_method = "paypal"
                    },
                    transactions = new List<Transaction>
                    {
                        new Transaction
                        {
                            description = $"BinaShop Dreamcatchers Purchase",
                            amount = new Amount
                            {
                                currency = "ILS",
                                total = (order.Total/1M).ToString(), // PayPal expects string amounts, eg. "20.00",
                                details = new Details()
                                {
                                    subtotal = (order.Subtotal/1M).ToString(),
                                    shipping = (order.Shipping/1M).ToString(),
                                    tax = (order.Tax/1M).ToString()
                                }
                            },
                            item_list = new ItemList()
                            {
                                items =
                                    order.OrderItems.Select(x => new Item()
                                    {
                                        description = x.ProductName,
                                        currency = "ILS",
                                        quantity = x.Quantity.ToString(),
                                        price = (x.Price/1M).ToString(), // PayPal expects string amounts, eg. "20.00"
                                    }).ToList()
                            }
                        }
                    },
                    redirect_urls = new RedirectUrls
                    {
                        return_url = Url.Action("Return", "Basket", null, Request.Url.Scheme),
                        cancel_url = Url.Action("Cancel", "Basket", null, Request.Url.Scheme)
                    }
                };

                // Send the payment to PayPal
                var createdPayment = payment.Create(apiContext);

                // Save a reference to the paypal payment
                order.PayPalReference = createdPayment.id;
                orders.Commit();

                // Find the Approval URL to send our user to
                var approvalUrl =
                createdPayment.links.FirstOrDefault(
                    x => x.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase));

                // Send the user to PayPal to approve the payment
               
                return Redirect(approvalUrl.href);
            }
            
            return RedirectToAction("Index");
        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public async Task<ActionResult> Return(string payerId, string paymentId)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            // Fetch the existing order
            var order = orders.Collection().FirstOrDefault(x => x.PayPalReference == paymentId);
            
            // Get PayPal API Context using configuration from web.config
            var apiContext = PayPalConfiguration.GetAPIContext();

            // Set the payer for the payment
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };

            // Identify the payment to execute
            var payment = new Payment()
            {
                id = paymentId
            };

            // Execute the Payment
            var executedPayment = payment.Execute(apiContext, paymentExecution);
            


            order.OrderStatus = "התשלום בוצע.";
            order.City = executedPayment.payer.payer_info.shipping_address.city;
            order.ZipCode = executedPayment.payer.payer_info.shipping_address.postal_code;
            order.Street = executedPayment.payer.payer_info.shipping_address.line1+ executedPayment.payer.payer_info.shipping_address.line2;
            order.Email = executedPayment.payer.payer_info.email;
            order.FirstName = executedPayment.payer.payer_info.first_name;
            order.LastName = executedPayment.payer.payer_info.last_name;
            orders.Commit();

            basketService.ClearBasket(this.HttpContext);
            var body = "תודה על הזמנתך, מספר ההזמנה שלך לבירורים הוא-"+ "<br/>" + order.Id+"<br/>"+"הודעה נוספת תשלח כשהמוצר ייצא למשלוח.";
            var message = new MailMessage();
            message.To.Add(new MailAddress(User.Identity.Name));  // replace with valid value 
            message.From = new MailAddress("postmaster@halohdim.com", "הלוכדים של בינה");  // replace with valid value
            message.Subject = "הזמנה מס'- "+order.Id;
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


            var body1 = "התקבלה הזמנה חדשה באתר, מספר הזמנה -"+ "<br/>" + order.Id;
            var message1 = new MailMessage();
            message1.To.Add(new MailAddress("postmaster@halohdim.com"));  // replace with valid value 
            message1.From = new MailAddress("postmaster@halohdim.com", "הלוכדים של בינה");  // replace with valid value
            message1.Subject = "התקבלה הזמנה חדשה" + order.Id;
            message1.Body = string.Format(body1);
            message1.IsBodyHtml = true;

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
                await smtp.SendMailAsync(message1);
            }
            return RedirectToAction("Thankyou", new { OrderId = order.Id });
        }
    }
}