using BL;
using BookStore.Bl;
using BookStore.BL;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class SalesController : Controller
    {
        IBook oClsBook;
        IGovernorate oClsGovernorate;
        IDeliveryMan oClsDeliveryMan;
        ISalesInvoiceBooks oClsSalesInvoicebook;
        ICustomerInvoiceInfo oClsCustomerInvoiceInfo;
        ISalesInvoice oClsSalesInvoice;
        UserManager<ApplicationUser> _userManager;
        public SalesController(IBook book,IGovernorate governorate, ISalesInvoiceBooks salesInvoicebook,IDeliveryMan deliveryMan, 
            ICustomerInvoiceInfo customerInvoiceInfo, ISalesInvoice salesInvoice, UserManager<ApplicationUser> userManager)
        {
            oClsBook = book;
            oClsGovernorate = governorate;
            oClsDeliveryMan = deliveryMan;
            oClsSalesInvoicebook = salesInvoicebook;
            oClsCustomerInvoiceInfo = customerInvoiceInfo;
            oClsSalesInvoice = salesInvoice;
            _userManager = userManager;
        }
        public IActionResult List()
        {
            return View(oClsSalesInvoice.GetAllData());
        }
        public IActionResult Edit(int? id)
        {
            ViewBag.lstGovernorate = oClsGovernorate.GetAll();
            ViewBag.lstBook2 = oClsBook.GetAll().Where(a=>a.Qty > 0).ToList();
            ShoppingCart sales = new ShoppingCart();
            if (id == null)
            {
                sales.lstBooks.Add(new ShoppingCartBook()
                {
                    BookId = 1,
                    SalesQty = 1,                    
                });
            }
            else
            {
                var invoice = oClsSalesInvoicebook.GetlstSalesInvoiceBook(Convert.ToInt32(id));
                List<ShoppingCartBook> shoppingCartBooks = new List<ShoppingCartBook>();
                foreach (var book in invoice)
                {
                    shoppingCartBooks.Add(new ShoppingCartBook()
                    {
                        InvoiceBookId = book.InvoiceBookId,
                        BookId = book.BookId,
                        SalesQty = book.Qty,
                        BookQty = book.BookQty,
                        Title = book.Title,
                        Price = book.SalesPrice-((book.SalesPrice*book.DiscountPercent)/100),                       
                    });
                }
                sales = new ShoppingCart()
                {
                    GovernorateId = invoice[0].GovernorateId,
                    Total = invoice[0].TotalPrice,
                    lstBooks = shoppingCartBooks,
                    isNew = false,
                    invoiceId= invoice[0].InvoiceId,
                    CustomerDeliverId = invoice[0].CustomerDeliverId,
                    Name = invoice[0].CutomerName,
                    Phone = invoice[0].PhoneNumber,
                    Address = invoice[0].Adress
                };
            }

            return View(sales);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(ShoppingCart model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.lstGovernorate = oClsGovernorate.GetAll();
                ViewBag.lstBook2 = oClsBook.GetAll().Where(a => a.Qty > 0).ToList();
                return View("Edit", model);
            }
            bool result = await SaveInvoice(model);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
        public async Task<bool> SaveInvoice(ShoppingCart model)
        {
            TbCustomerDeliverInfo customerDeliverInfo = new TbCustomerDeliverInfo()
            {
                CustomerDeliverId = model.CustomerDeliverId,
                CutomerName = model.Name,
                PhoneNumber = model.Phone,
                Adress = model.Address,
                GovernorateId = model.GovernorateId,
            };

            List<TbSalesInvoiceBook> lstSalesInvoseBook = new List<TbSalesInvoiceBook>();
            foreach (var book in model.lstBooks)
            {
                lstSalesInvoseBook.Add(new TbSalesInvoiceBook
                {
                    InvoiceBookId = book.InvoiceBookId ??= 0,
                    BookId = book.BookId,
                    Qty = book.SalesQty,
                    Price = oClsBook.GetPriceAfterDiscount(book.BookId) * book.SalesQty                   
                });
                
            }
            var user = await _userManager.GetUserAsync(User);
            TbSalesInvoice salesInvoice = new TbSalesInvoice()
            {
                InvoiceId = model.invoiceId,
                CustomerId = user.Id,
                CurrentState = 1,
                InvoiceDate = DateTime.Now,
                TotalPrice = lstSalesInvoseBook.Sum(a => a.Price) + oClsGovernorate.GetById(model.GovernorateId).DeliveryPrice,
                CreatedBy = user.Id,
                DeliveryManId = oClsDeliveryMan.GetDeliverManId(),
                DeliveryState = "InPrograss",
                DeliveryDate = DateTime.Now.AddDays(5)
            };
            customerDeliverInfo.UserId = user.Id;
            return oClsCustomerInvoiceInfo.Save(customerDeliverInfo, salesInvoice, lstSalesInvoseBook,model.isNew);
        }
        public IActionResult ShowDetails(int id)
        {
            var Invoice = oClsSalesInvoicebook.GetlstSalesInvoiceBook(id);
            return View(Invoice ??= new List<Domains.VwSalesInvoiceBook>());
        }
        public IActionResult Delete(int id)
        {
            bool result = oClsSalesInvoice.Delete(id);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
    }
}
