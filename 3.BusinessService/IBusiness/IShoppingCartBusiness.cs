using BusinessService.Models;
using Database.Model;

namespace BusinessService.IBusiness
{
    public interface IShoppingCartBusiness
    {
        ShoppingCartModel AddToCart(AddItemToShoppingCartModel param, bool isAdmin = false);
        ShoppingCartModel GetCurrentCart(int cartId);
        ShoppingCartModel UpdateCurrentCart(AddItemToShoppingCartModel param);
        ShoppingCartModel UpdateCurrentCartForAdmin(AddItemToShoppingCartModel param);
        ShoppingCartModel ApplyPromotionCode(int cartId, PromotionModel promo);
        ShoppingCartModel RemovePromotion(int cartId);
        ShoppingCartModel RemoveShippingPromotion(int cartId);
        ShoppingCart CalculateShoppingCart(ShoppingCart cart);
        bool DeleteAllUnusedShoppingCart();
    }
}
