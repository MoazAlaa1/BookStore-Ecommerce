using BookStore.BL;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        ICategory oClsCategory;
        IBook oClsBook;
        public CategoryController(ICategory category, IBook book)
        {
            oClsCategory = category;
            oClsBook = book;
        }
        public IActionResult List()
        {
            var categories = oClsCategory.GetAll();
            return View(categories);
        }
        public IActionResult CategoryBooks(int id,string name)
        {
            ViewBag.CategoryName = name;
            ViewBag.CategoryId = id;
            return View();
        }
    }
}
