using BookStore.BL;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class GovernorateController : Controller
    {
        IGovernorate oClsGovernorate;
        public GovernorateController(IGovernorate Governorate)
        {
            oClsGovernorate = Governorate;
        }
        public IActionResult List()
        {
            return View(oClsGovernorate.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                return View(oClsGovernorate.GetById(Convert.ToInt32(id)));
            }
            return View(new TbGovernorate());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbGovernorate model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            bool result = await oClsGovernorate.Save(model);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            bool result = oClsGovernorate.Delete(id);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
    }
}
