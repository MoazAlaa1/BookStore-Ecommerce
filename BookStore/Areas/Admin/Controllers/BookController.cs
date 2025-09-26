using BookStore.BL;
using BookStore.Models;
using Context_form_database.Utlities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BookController : Controller 
    {
        IBook oClsBook;
        ICategory oClsCategory;
        IAuthor oClsAuthor;
        IPublisher oClsPublisher;
        IDiscount oClsDiscount;
        UserManager<ApplicationUser> _userManager;
        public BookController(IBook Book,ICategory category,IPublisher publisher,IAuthor author, IDiscount discount,
                                 UserManager<ApplicationUser> userManager)
        {
            oClsBook = Book;
            oClsCategory = category;
            oClsAuthor = author;
            oClsPublisher = publisher;
            oClsDiscount = discount;
            _userManager = userManager;
        }
        public IActionResult List()
        {
            var Books = oClsBook.GetAllVwBooks();
            return View(Books);
        }
        public IActionResult Edit(int? id)
        {
            var book = new TbBook();            
            ViewBag.lstCategories = oClsCategory.GetAll();
            ViewBag.lstAuthors = oClsAuthor.GetAll();
            ViewBag.lstPublishers = oClsPublisher.GetAll();
            ViewBag.lstDiscounts = oClsDiscount.GetAll();
            //ViewBag.lstDiscount = lstBooks.Select(a=> new { a.DiscountId , a.DiscountPercent}).Distinct().OrderBy(a=>a.DiscountPercent).ToList();
            if (id != null)
            {
               book = oClsBook.GetById(Convert.ToInt32(id));
            }            
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbBook book, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.lstCategories = oClsCategory.GetAll();
                ViewBag.lstAuthors = oClsAuthor.GetAll();
                ViewBag.lstPublishers = oClsPublisher.GetAll();
                ViewBag.lstDiscounts = oClsDiscount.GetAll();
                return View("Edit", book);
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                book.ImageName = await Helper.UploadImage(Files, "Books");
                bool result = oClsBook.Save(book,user.Id);
                if (result == false)
                    return Redirect("/Error/E500?type=Admin");
                return RedirectToAction("List");
            }

        }
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await oClsBook.Delete(id);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
        
    }
}
