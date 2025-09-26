using BookStore.BL;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BookStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PublishController : Controller
    {
        IPublisher oClsPublisher;
        public PublishController(IPublisher Publisher)
        {
            oClsPublisher = Publisher;
        }
        public IActionResult List()
        {
            return View(oClsPublisher.GetAll());
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                return View(oClsPublisher.GetById(Convert.ToInt32(id)));
            }
            return View(new TbPublish());
        }
        public async Task<IActionResult> Save(TbPublish model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            bool result = await oClsPublisher.Save(model);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            bool result = oClsPublisher.Delete(id);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return RedirectToAction("List");
        }
    }
}
