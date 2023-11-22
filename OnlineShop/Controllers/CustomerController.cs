using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
