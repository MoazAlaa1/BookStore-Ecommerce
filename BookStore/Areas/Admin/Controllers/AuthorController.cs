using BookStore.BL;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BookStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AuthorController : Controller
    {
        IAuthor oClsAuthor;
        public AuthorController(IAuthor author)
        {
            oClsAuthor = author;
        }
        public IActionResult List()
        {
            return View(oClsAuthor.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                return View(oClsAuthor.GetById(Convert.ToInt32(id)));
            }
            return View(new TbAuthor());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(TbAuthor model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            bool result = oClsAuthor.Save(model);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            bool result = oClsAuthor.Delete(id);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
    }
}
