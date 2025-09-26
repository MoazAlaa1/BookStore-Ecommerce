using BookStore.Bl;
using BookStore.Models;
using Context_form_database.Utlities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        ISliders oClsSlider;
        UserManager<ApplicationUser> _userManager;
        public SliderController(ISliders slider, UserManager<ApplicationUser> userManager)
        {
            oClsSlider = slider;
            _userManager = userManager;

        }
        public IActionResult List()
        {
            return View(oClsSlider.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                return View(oClsSlider.GetById(Convert.ToInt32(id)));
            }
            return View(new TbSlider());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbSlider model, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);
            model.ImageName = await Helper.UploadImage(files, "Sliders");
            var user = await _userManager.GetUserAsync(User);
            bool result = oClsSlider.Save(model, user.Id);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            bool result = oClsSlider.Delete(id);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
    }
}

