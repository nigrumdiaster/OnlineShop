using OnlineShop.Models;

namespace OnlineShop.ViewModels
{
    public class CartItemViewModel
    {
        public Product product { get; set; }
        public int amount { get; set; }
        public double TotalMoney => amount * product.PromotionalPrice;
    }
}
