using BookStore.Bl;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        ISalesInvoice oClsSalesInvoice;
        IPurchaseInvoice oClsPurchaseInvoice;
        UserManager<ApplicationUser> _userManager;
        public HomeController(ISalesInvoice salesInvoice, IPurchaseInvoice purchaseInvoice, UserManager<ApplicationUser> userManager)
        {
            oClsSalesInvoice = salesInvoice;
            oClsPurchaseInvoice = purchaseInvoice;
            _userManager = userManager; 
        }
        public IActionResult Index()
        {
            var sales = oClsSalesInvoice.GetAllData();
            var users = _userManager.Users.ToList();
            VmDashboard vmDashboard = new VmDashboard();
            if(sales.Count>0)
            {
                vmDashboard.lstSales = sales.OrderByDescending(a => a.InvoiceDate).Take(4).ToList();
                vmDashboard.TotalSales = (sales.Sum(a => a.TotalPrice));
                vmDashboard.TotalOrder = sales.Count();
                vmDashboard.TotalCustomer = users.Count();
            }

            return View(vmDashboard);
        }
    }
}
