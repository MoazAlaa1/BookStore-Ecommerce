using BookStore.Bl;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class PagesController : Controller
    {
        IPages oClsPages;
        public PagesController(IPages pages)
        {
                oClsPages = pages;
        }
        public IActionResult Index(int id)
        {
            var pages = oClsPages.GetById(id);
            return View(pages);
        }
    }
}
