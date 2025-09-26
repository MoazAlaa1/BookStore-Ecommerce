using BookStore.Bl;
using BookStore.BL;
using BookStore.Models;
using Context_form_database.Utlities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        IPages oClsPages;
        UserManager<ApplicationUser> _userManager;
        public PagesController(IPages pages, UserManager<ApplicationUser> userManager)
        {
            oClsPages = pages;
            _userManager = userManager;

        }
        public IActionResult List()
        {
            return View(oClsPages.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                return View(oClsPages.GetById(Convert.ToInt32(id)));
            }
            return View(new TbPages());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbPages model, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);
            model.ImageName = await Helper.UploadImage(files, "Pages");
            var user = await _userManager.GetUserAsync(User);
            bool result = oClsPages.Save(model,user.Id);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            bool result = oClsPages.Delete(id);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
    }
}
