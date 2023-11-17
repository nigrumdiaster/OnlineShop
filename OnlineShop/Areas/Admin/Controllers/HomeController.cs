using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly OnlineShopContext _context;
        public HomeController(OnlineShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            int year = DateTime.Now.Year;
            ViewBag.totalRevenue = string.Format("{0:0,0} VND", totalRevenue(year));
            ViewBag.averageRevenue = string.Format("{0:0,0} VND", totalRevenue(year)/12);
            ViewBag.growth = string.Format("{0:0.00}", (totalRevenue(year - 1) - totalRevenue(year - 2)) / totalRevenue(year - 2) * 100);
            ViewBag.totalUsers = totalUsers();
            ViewBag.totalOrders = totalOrders();
            List<string> montlyRevenueLst = new List<string>();
            for (int i = 1; i <=12; i++)
            {
                montlyRevenueLst.Add(string.Format("{0:0,0} VND", monthlyRevenue(i, year)));
            }
            ViewBag.monthlyRevenue = montlyRevenueLst;
            return View();
        }
        public decimal totalRevenue(int year)
        {
            var lst = from s1 in (from s1 in (from s1 in _context.Orders.Where(s1 => s1.IsPay == 1 
                                                                               && s1.IsDeleted == 0
                                                                               && s1.Date.Value.Year == year)
                                              join s2 in _context.OrderItems
                                              on s1.OrderId equals s2.OrderId
                                              select new
                                              {
                                                  s1.OrderId,
                                                  s1.Date,
                                                  s2.ProductId,
                                                  s2.Count
                                              })
                                  join s2 in _context.Products
                                  on s1.ProductId equals s2.ProductId
                                  select new
                                  {
                                      s1.Count,
                                      s2.PromotionalPrice
                                  }
                      )
                      select new
                      {
                          revenue = s1.Count * s1.PromotionalPrice
                      };
            return decimal.Parse(lst.Sum(n => n.revenue).ToString());
        }
        public decimal monthlyRevenue(int month, int year)
        {
            if (_context.Orders.Where(n => n.Date.Value.Year == year && n.Date.Value.Month == month).Count() > 0)
            {
                var lst = from s1 in (from s1 in (from s1 in _context.Orders.Where(s1 => s1.IsPay == 1 
                                                                                    && s1.IsDeleted == 0
                                                                                    && s1.Date.Value.Month == month
                                                                                    && s1.Date.Value.Year == year)
                                                  join s2 in _context.OrderItems
                                                  on s1.OrderId equals s2.OrderId
                                                  select new
                                                  {
                                                      s1.OrderId,
                                                      s1.Date,
                                                      s2.ProductId,
                                                      s2.Count
                                                  })
                                      join s2 in _context.Products
                                      on s1.ProductId equals s2.ProductId
                                      select new
                                      {
                                          s1.Count,
                                          s2.PromotionalPrice
                                      }
                          )
                          select new
                          {
                              revenue = s1.Count * s1.PromotionalPrice
                          };
                return decimal.Parse(lst.Sum(n => n.revenue).ToString());
            }
            return 0;
        }
        public int totalUsers()
        {
            return _context.Users.Count();
        }
        public int totalOrders()
        {
            return _context.Orders.Count();
        }
    }
}
