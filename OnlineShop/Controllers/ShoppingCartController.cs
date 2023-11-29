using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using OnlineShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using OnlineShop.Extension;


namespace OnlineShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly OnlineShopContext _context;
        private readonly ILogger<ShoppingCartController> _logger;

        public ShoppingCartController(OnlineShopContext context, ILogger<ShoppingCartController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<CartItemViewModel> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItemViewModel>>("GioHang");
                if (gh == default(List<CartItemViewModel>))
                {
                    gh = new List<CartItemViewModel>();
                }
                return gh;
            }
        }

        [HttpPost]
        public IActionResult AddToCart(int productID, int? count)
        {
            List<CartItemViewModel> gioHang = GioHang;

            try
            {
                CartItemViewModel item = GioHang.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null)
                {
                    if (count.HasValue)
                    {
                        item.amount = count.Value;
                    }
                    else
                    {
                        item.amount++;
                    }
                }
                else
                {
                    Product hh = _context.Products.SingleOrDefault(p => p.ProductId == productID);
                    item = new CartItemViewModel
                    {
                        amount = count.HasValue ? count.Value : 1,
                        product = hh
                    };
                    gioHang.Add(item);
                }

                HttpContext.Session.Set<List<CartItemViewModel>>("GioHang", gioHang);

                TempData["SuccessMessage"] = "Product added to cart successfully.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product to cart.");
                TempData["ErrorMessage"] = "Error adding the product to the cart.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Remove(int productID)
        {
            List<CartItemViewModel> gioHang = GioHang;

            try
            {
                CartItemViewModel item = gioHang.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null)
                {
                    gioHang.Remove(item);
                }

                HttpContext.Session.Set<List<CartItemViewModel>>("GioHang", gioHang);

                TempData["SuccessMessage"] = "Product removed from cart successfully.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing product from cart.");
                TempData["ErrorMessage"] = "Error removing the product from the cart.";
                return RedirectToAction("Index");
            }
        }

        [Route("cart.html", Name = "Cart")]
        public IActionResult Index()
        {
            var lsGioHang = GioHang;
            return View(GioHang);
        }
    }
}
