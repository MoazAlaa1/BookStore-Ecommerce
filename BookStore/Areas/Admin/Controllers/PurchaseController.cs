using BookStore.Bl;
using BookStore.BL;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BookStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PurchaseController : Controller
    {
        IBook oClsBook;
        ISupplier oClsSupplier;
        IPurchaseInvoice oClsPurchaseInvoice;
        IPurchaseInvoiceBooks oClsPurchaseInvoiceBook;
        UserManager<ApplicationUser> _userManager;
        public PurchaseController(IBook book,ISupplier supplier, IPurchaseInvoice purchaseInvoice,
                                    IPurchaseInvoiceBooks purchaseInvoiceBook, UserManager<ApplicationUser> userManager)
        {
            oClsBook = book;
            oClsSupplier = supplier;
            oClsPurchaseInvoice = purchaseInvoice;
            oClsPurchaseInvoiceBook = purchaseInvoiceBook;
            _userManager = userManager;

        }
        public IActionResult List()
        {
             return View(oClsPurchaseInvoice.GetAllVw());
        }
        public IActionResult Edit(int? id)
        {
            
            ViewBag.lstSuppliers = oClsSupplier.GetAll();
            ViewBag.lstBook2 = oClsBook.GetAll();
            PurchaseCart purchase = new PurchaseCart();
            if(id == null)
            {
                purchase.lstBooks.Add(new PurchaseCartBook()
                {
                    BookId = 1,
                    PurchaseQty = 1,                   
                });
            }
            else
            {
                var invoice = oClsPurchaseInvoiceBook.GetlstPurchaseInvoiceBook(Convert.ToInt32(id));
                List<PurchaseCartBook> purchaseCartBook = new List<PurchaseCartBook>();
                foreach (var book in invoice)
                {
                    purchaseCartBook.Add(new PurchaseCartBook()
                    {   InvoiceBookId = book.InvoiceBookId,
                        BookId = book.BookId,
                        PurchaseQty = book.Qty,
                        Title = book.Title,
                        Price = book.PurchasePrice
                    });

                }
                purchase = new PurchaseCart()
                {
                    SupplierId = invoice[0].SupplierId,
                    Total = invoice[0].TotalPrice,
                    lstBooks = purchaseCartBook,
                    isNew = false,
                    invoiceId= invoice[0].InvoiceId,
                };
            }

            return View(purchase);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(PurchaseCart model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.lstSuppliers = oClsSupplier.GetAll();
                ViewBag.lstBook2 = oClsBook.GetAll();
                return View("Edit", model);
            }
            bool result = await SaveInvoice(model);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }

        public async Task<bool> SaveInvoice(PurchaseCart model)
        {
            List<TbPurchaseInvoiceBook> lstPurchaseInvoseBook = new List<TbPurchaseInvoiceBook>();
            foreach(var book in model.lstBooks)
            {
                lstPurchaseInvoseBook.Add(new TbPurchaseInvoiceBook
                {
                    InvoiceBookId = book.InvoiceBookId??=0,
                    BookId=book.BookId,
                    Qty = book.PurchaseQty,
                    Price = book.Price * book.PurchaseQty
                });               
            }
            var user = await _userManager.GetUserAsync(User);
            TbPurchaseInvoice PurchaseInvoice = new TbPurchaseInvoice()
            {
                PurchaseInvoiceId = model.invoiceId,
                SupplierId = model.SupplierId,
                CurrentState = 1,
                InvoiceDate = DateTime.Now,
                CreatedBy = user.Id,
                TotalPrice = lstPurchaseInvoseBook.Sum(a => a.Price)
            };

            return oClsPurchaseInvoice.Save(PurchaseInvoice, lstPurchaseInvoseBook,model.isNew);  
        }
        public IActionResult ShowDetails(int id)
        {
            var Invoice = oClsPurchaseInvoiceBook.GetlstPurchaseInvoiceBook(id);
            return View(Invoice??=new List<Domains.VwPurchaseInvoiceBook>());
        }
        public IActionResult Delete(int id)
        {
            bool result = oClsPurchaseInvoice.Delete(id);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }


    }
}
