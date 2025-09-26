using BookStore.BL;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using BookStore.Bl;
using Newtonsoft.Json;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        IBook oClsBook;
        ISliders oClsSlider;
        ICategory oClsCategory;

        public HomeController(IBook book, ISliders slider, ICategory category)
        {
            oClsBook = book;
            oClsSlider = slider;
            oClsCategory = category;
        }
        public IActionResult Index()
        {
            var Books = oClsBook.GetAll();
            VmHomePages vm = new VmHomePages();
            vm.lstAllBooks = Books.Skip(10).Take(18).ToList();
            vm.lstRecommendedBooks = Books.Skip(28).Take(10).ToList();
            vm.lstFreeDelivery = Books.Skip(38).Take(3).ToList();
            vm.lstNewBooks = Books.Skip(41).Take(3).ToList();
            vm.lstCategories = oClsCategory.GetAll().Where(a=>a.ShowInHomePage==true).Take(3).ToList();
            vm.lstSliders = oClsSlider.GetAll();
            return View(vm);
        }
        public IActionResult Search(string searchItem)
        {
            //var books = new List<TbBook>();
            //if (!string.IsNullOrEmpty(searchTerm))
            //{
            //    books = oClsBook.Search(searchTerm);
            //}
            ViewBag.SearchItem = searchItem;
            return View();
        }
    }
}
