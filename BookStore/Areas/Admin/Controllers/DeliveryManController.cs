using BookStore.BL;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DeliveryManController : Controller
    {
        IDeliveryMan oClsDeliveryMan;
        public DeliveryManController(IDeliveryMan DelieryMan)
        {
            oClsDeliveryMan = DelieryMan;
        }
        public IActionResult List()
        {
            return View(oClsDeliveryMan.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                return View(oClsDeliveryMan.GetById(Convert.ToInt32(id)));
            }
            return View(new TbDeliveryMan());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbDeliveryMan model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            bool result = await oClsDeliveryMan.Save(model);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            bool result = oClsDeliveryMan.Delete(id);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
    }
}
