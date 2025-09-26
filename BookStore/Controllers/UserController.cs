using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class UserController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        BookStoreContext context;
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, BookStoreContext ctx) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            context = ctx;
        }
        public IActionResult Login(string returnUrl)
        {
            UserModel user = new UserModel()
            {
                ReturnUrl = returnUrl
            };
            return View(user);
        }
        public IActionResult Register(string returnUrl)
        {
            UserModel user = new UserModel()
            {
                ReturnUrl = returnUrl
            };
            return View(user);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(UserModel model)
        {
            if (!ModelState.IsValid)
                return View("Register", model);

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var MyUser = await _userManager.FindByEmailAsync(user.Email);
                    await _userManager.AddToRoleAsync(MyUser,"Customer");
                    var loginResult = await _signInManager.PasswordSignInAsync(user.Email, model.Password, true, true);
                    if (loginResult.Succeeded)
                    {
                        if (string.IsNullOrEmpty(model.ReturnUrl))
                        {
                            return Redirect("/Home/Index");
                        }
                        else
                        {
                            return Redirect(model.ReturnUrl);
                        }
                    }

                }
                else 
                {
                    return Redirect("/User/Register");
                }
            }
            catch 
            {
                return Redirect("/Error/E500");
            }
            return View(new UserModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel model)
        {

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email
            };
            try
            {
                var loginResult = await _signInManager.PasswordSignInAsync(user.Email, model.Password, true, true);
                if (loginResult.Succeeded)
                {
                    if(string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        return Redirect(model.ReturnUrl);
                    }                    
                }
                else
                {
                    return Redirect("/User/Login");
                }
            }
            catch
            {
                return Redirect("/Error/E500");
            }
            
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }

    }
}
