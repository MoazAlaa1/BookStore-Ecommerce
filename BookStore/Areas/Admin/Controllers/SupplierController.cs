using BookStore.BL;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class SupplierController : Controller
    {
        ISupplier oClsSupplier;
        public SupplierController(ISupplier supplier)
        {
            oClsSupplier = supplier;
        }
        public IActionResult List()
        {
            return View(oClsSupplier.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                return View(oClsSupplier.GetById(Convert.ToInt32(id)));
            }
            return View(new TbSupplier());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(TbSupplier model)
        {
            if (!ModelState.IsValid) 
                return View("Edit",model);

            oClsSupplier.Save(model);
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            oClsSupplier.Delete(id);
            return RedirectToAction("List");
        }
    }
}
