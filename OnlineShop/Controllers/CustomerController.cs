using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class CustomerController : Controller
    {
        private readonly OnlineShopContext _context;

        public CustomerController(OnlineShopContext context)
        {
            _context = context;
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn([Bind("Email", "Password")] User user)
        {
            if (ModelState.IsValid)
            {
                List<User> lst = _context.Users.Where(n => n.Email == user.Email
                                                        && n.Password == user.Password).ToList();
                                                        //&& n.Password == encryptPassword(user.Password)).ToList();
                if (lst.Count() > 0)
                {
                    HttpContext.Session.SetInt32("userId", lst[0].UserId);
                    if (lst[0].RoleId == 1)
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (lst[0].RoleId == 2) {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
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
        public string encryptPassword(string password)
        {
            if(password == null)
            {
                return "";
            }
            string key = "@a1235&%%@Dacxs";
            password += key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
    }
}
