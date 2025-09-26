using BookStore.Bl;
using BookStore.Models;
using Context_form_database.Utlities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        ISettings oClsSetting;
        public SettingController(ISettings setting)
        {

            oClsSetting = setting;
        }
        public IActionResult Edit()
        {
            TbSettings setting = oClsSetting.GetAll();
            return View((setting)??= new TbSettings());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbSettings model, List<IFormFile> Files1 , List<IFormFile> Files2, List<IFormFile> Files3)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);

            model.Logo =  await Helper.UploadImage(Files1, "Settings");
            model.MiddlePanner =  await Helper.UploadImage(Files2, "Settings");
            model.LastPanner =  await Helper.UploadImage(Files3, "Settings");
            bool result = oClsSetting.Save(model);
            if (result == false)
                return Redirect("/Error/E500?type=Admin");
            return Redirect("/Admin/Home/Index");
        }
    }
}
