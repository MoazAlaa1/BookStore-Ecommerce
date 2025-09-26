using BookStore.BL;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DiscountController : Controller
    {
        IDiscount oClsDiscount;
        public DiscountController(IDiscount discount)
        {
            oClsDiscount = discount;
        }
        public IActionResult List()
        {
            return View(oClsDiscount.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                return View(oClsDiscount.GetById(Convert.ToInt32(id)));
            }
            return View(new TbDiscount());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbDiscount model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            bool result = await oClsDiscount.Save(model);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            bool result = oClsDiscount.Delete(id);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
    }
}
