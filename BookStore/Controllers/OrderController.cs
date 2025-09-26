using BL;
using BookStore.Bl;
using BookStore.BL;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Microsoft.AspNetCore.Authorization;
using Domains;
using BookStore.Filters;

namespace BookStore.Controllers
{
    public class OrderController : Controller
    {
        IBook oClsBook;
        IGovernorate oClsGovernorate;
        IDeliveryMan oClsDeliveryMan;
        ICustomerInvoiceInfo oClsCustomerInvoiceInfo;
        ISalesInvoice oClsSalesInvoice;
        ISalesInvoiceBooks oClsSalesInvoiceBook;
        UserManager<ApplicationUser> _userManager;

        public OrderController(IBook book,IGovernorate governorate, IDeliveryMan deliveryMan, ISalesInvoice salesInvoice,
        ICustomerInvoiceInfo customerInvoiceInfo, ISalesInvoiceBooks salesInvoiceBook, UserManager<ApplicationUser> userManager)
        {
            oClsBook = book;
            oClsGovernorate = governorate;
            oClsDeliveryMan = deliveryMan;
            oClsCustomerInvoiceInfo = customerInvoiceInfo;
            _userManager = userManager;
            oClsSalesInvoice = salesInvoice;
            oClsSalesInvoiceBook = salesInvoiceBook;
        }
        public IActionResult MyCart()
        {
            var cart = new ShoppingCart();
            if (HttpContext.Request.Cookies["cart"] != null)
            {
                cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["cart"]); 
            }
            return View(cart);
        }

        public IActionResult AddToCart(int bookId)
        {
            ShoppingCart cart;
            if (HttpContext.Request.Cookies["cart"] != null)
            {
                cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["cart"]);
            }
            else
            {
                cart = new ShoppingCart();
            }
            decimal PriceAfterDiscount = oClsBook.GetPriceAfterDiscount(bookId);
            var book = oClsBook.GetById(bookId);        
            var bookInList = cart.lstBooks.Where(a => a.BookId == bookId).FirstOrDefault();
            if (bookInList != null)
            {
                bookInList.SalesQty++;
                bookInList.Total = bookInList.SalesQty * PriceAfterDiscount;
            }
            else
            {
                cart.lstBooks.Add(new ShoppingCartBook
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    ImageName = book.ImageName,
                    Price = PriceAfterDiscount,
                    SalesQty = 1,
                    BookQty = book.Qty,
                    Total = PriceAfterDiscount
                });                
            }
            cart.SubTotal = cart.lstBooks.Sum(a => a.Total);
            HttpContext.Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart));
            //return Redirect(Request.Headers["Referer"].ToString());
            return RedirectToAction("MyCart");
        }
        public IActionResult DeleteFromCart(int id)
        {
            ShoppingCart cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["cart"]);
            var Book = cart.lstBooks.Where(a => a.BookId == id).FirstOrDefault();
            var price = Book.Total;
            cart.lstBooks.Remove(Book);
            cart.SubTotal -= price;
            HttpContext.Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("MyCart");
        }

        [Authorize]
        [Accessable]
        public IActionResult CustomerInvoiceInfo()
        {
            ViewBag.lstGovernorates = oClsGovernorate.GetAll();
            return View(new TbCustomerDeliverInfo());
        }

        [Authorize]
        [Accessable]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Payment(TbCustomerDeliverInfo customerDetails)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.lstGovernorates = oClsGovernorate.GetAll();
                return View("CustomerInvoiceInfo", customerDetails);
            }
            VmCustomerInvoiceInfo vm = new VmCustomerInvoiceInfo();
            vm.ShoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["cart"]);
            vm.ShoppingCart.ShippingCost = oClsGovernorate.GetById(customerDetails.GovernorateId).DeliveryPrice;
            vm.ShoppingCart.Total = vm.ShoppingCart.SubTotal + vm.ShoppingCart.ShippingCost;
            HttpContext.Response.Cookies.Append("cart", JsonConvert.SerializeObject(vm.ShoppingCart));
            vm.CustomerDetails = customerDetails;
            HttpContext.Session.SetString("customerInfo", JsonConvert.SerializeObject(customerDetails));
            
            return View(vm);
        }
        [Authorize]
        [Accessable]
        public async Task<IActionResult> SuccedOrders()
        {
            ShoppingCart cart;
            TbCustomerDeliverInfo info;
            if (HttpContext.Request.Cookies["cart"] != null || HttpContext.Session.GetString("customerInfo") != null)
            {
                cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["cart"]);
                info = JsonConvert.DeserializeObject<TbCustomerDeliverInfo>(HttpContext.Session.GetString("customerInfo"));
                bool result = await SaveOrder(info, cart);
                if (result == false)
                    return Redirect("/Error/E500");
            }
            return View();
        }

        public async Task<bool> SaveOrder(TbCustomerDeliverInfo info, ShoppingCart cart)
        {
            List<TbSalesInvoiceBook> olstInvoiceBook = new List<TbSalesInvoiceBook>();
            foreach (var Book in cart.lstBooks)
            {
                olstInvoiceBook.Add(new TbSalesInvoiceBook
                {
                    BookId = Book.BookId,
                    Qty = Book.SalesQty,
                    Price = Book.Price,
                });
            }
            var user = await _userManager.GetUserAsync(User);
            TbSalesInvoice oSalesInvoise = new TbSalesInvoice()
            {
                CurrentState = 1,
                CreatedBy = user.Id,
                InvoiceDate = DateTime.Now,
                CustomerId = user.Id,
                TotalPrice = cart.Total,
                DeliveryManId = oClsDeliveryMan.GetDeliverManId(),
                DeliveryState = "inPrograss",
                DeliveryDate = DateTime.Now.AddDays(5)
            };
            info.UserId = user.Id;
            bool result  = oClsCustomerInvoiceInfo.Save(info, oSalesInvoise, olstInvoiceBook, true);
            HttpContext.Response.Cookies.Delete("cart");
            HttpContext.Session.Remove("customerInfo");
            return result;
        }

        public async Task<IActionResult> MyOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            var invoices = oClsSalesInvoice.GetAll().Where(a => a.CustomerId == user.Id).ToList();
            if (invoices.Count == 0)
                return View(new List<TbSalesInvoice>());
            return View(invoices);
        }

        public IActionResult ShowOrderDetails(int id)
        {
            return View(oClsSalesInvoiceBook.GetlstSalesInvoiceBook(id));
        }
    }
}
