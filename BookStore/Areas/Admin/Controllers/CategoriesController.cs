using BookStore.BL;
using BookStore.Models;
using Context_form_database.Utlities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    public class CategoriesController : Controller
    {
        ICategory oClsCategory;
        public CategoriesController(ICategory category)
        {
            oClsCategory = category;
        }
        public IActionResult List()
        {
            return View(oClsCategory.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                return View(oClsCategory.GetById(Convert.ToInt32(id)));
            }
            else
            {
                return View(new TbCategory());
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbCategory model, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);
            model.ImageName = await Helper.UploadImage(files, "Categories");
            bool result = await oClsCategory.Save(model);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            bool result = oClsCategory.Delete(id);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("list");
        }

    }
}
