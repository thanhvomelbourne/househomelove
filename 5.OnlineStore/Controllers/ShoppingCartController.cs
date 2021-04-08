using BusinessService.IBusiness;
using BusinessService.Models;
using BusinessService.Service;
using CoreLibrary.Data;
using log4net;
using OnlineStore.Helper;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IService _service;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ShoppingCartController(IService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult AddToCart(AddItemToShoppingCartModel model)
        {
            try
            {
                ShoppingCartModel result = _service.ShoppingCart.AddToCart(model);

                UtilityHelper.SetCookie(UtilityHelper.SHOPPING_CART_ID, result.Id.ToString(), DateTime.UtcNow.AddDays(1));

                return Json(new { success = true, data = result, totalItem = result.CartItemList?.Sum(x => x.Quantity) ?? 0 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddToCartForAdmin(AddItemToShoppingCartModel model)
        {
            try
            {
                ShoppingCartModel result = _service.ShoppingCart.AddToCart(model, true);
                
                return Json(new { success = true, data = result, totalItem = result.CartItemList?.Sum(x => x.Quantity) ?? 0 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddWishlistToCart(AddItemToShoppingCartModel model)
        {
            try
            {
                if (model.CartId == 0)
                {
                    foreach (int i in model.ProductIds)
                    {
                        model.ProductId = i;
                        try
                        {
                            ShoppingCartModel result = _service.ShoppingCart.AddToCart(model);
                            model.CartId = result.Id;
                        }
                        catch (Exception ex)
                        {
                            log.Debug($"{ex.Message} - {ex}");
                        }
                    }

                    UtilityHelper.SetCookie(UtilityHelper.SHOPPING_CART_ID, model.CartId.ToString(), DateTime.UtcNow.AddDays(1));
                }
                else
                {
                    foreach (int i in model.ProductIds)
                    {
                        model.ProductId = i;
                        _service.ShoppingCart.AddToCart(model);
                    }
                }

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCurrentCart(int cartId)
        {
            try
            {
                ShoppingCartModel result = _service.ShoppingCart.GetCurrentCart(cartId);

                if (result == null)
                {
                    UtilityHelper.SetCookie(UtilityHelper.ORDER_ID, null, DateTime.UtcNow.AddDays(-1));
                    UtilityHelper.SetCookie(UtilityHelper.SHOPPING_CART_ID, null, DateTime.UtcNow.AddDays(-1));
                }

                return result == null ? Json(new { success = false }, JsonRequestBehavior.AllowGet) : Json(new { success = true, data = result, totalItem = result.CartItemList?.Sum(x => x.Quantity) ?? 0 }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                UtilityHelper.SetCookie(UtilityHelper.ORDER_ID, null, DateTime.UtcNow.AddDays(-1));
                UtilityHelper.SetCookie(UtilityHelper.SHOPPING_CART_ID, null, DateTime.UtcNow.AddDays(-1));
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateCurrentCart(AddItemToShoppingCartModel model)
        {
            try
            {
                ShoppingCartModel result = _service.ShoppingCart.UpdateCurrentCart(model);

                if (result == null)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, data = result, totalItem = result.CartItemList?.Sum(x => x.Quantity) ?? 0 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public JsonResult UpdateCurrentCartForAdmin(AddItemToShoppingCartModel model)
        {
            try
            {
                ShoppingCartModel result = _service.ShoppingCart.UpdateCurrentCartForAdmin(model);

                if (result == null)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ApplyPromotionCode(int cartId, string promotionCode)
        {
            try
            {
                if (cartId == 0)
                {
                    return Json(new { success = false, message = "You don't have any product in your cart yet, please add something and try again!" }, JsonRequestBehavior.AllowGet);
                }

                if (promotionCode != "REMOVE")
                {

                    PromotionModel promo = _service.Promotion.GetPromotionByPromotionCode(promotionCode);

                    if (promo == null)
                    {
                        return Json(new { success = false, message = "Your promotion code is not correct! Please Try again!" }, JsonRequestBehavior.AllowGet);
                    }

                    if (!promo.IsLive)
                    {
                        return Json(new { success = false, message = "Your promotion code is expired!" }, JsonRequestBehavior.AllowGet);
                    }

                    ShoppingCartModel applied = _service.ShoppingCart.ApplyPromotionCode(cartId, promo);

                    return Json(new { success = true, data = applied, totalItem = applied.CartItemList?.Sum(x => x.Quantity) ?? 0 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ShoppingCartModel applied = _service.ShoppingCart.RemovePromotion(cartId);

                    return Json(new { success = true, data = applied, totalItem = applied.CartItemList?.Sum(x => x.Quantity) ?? 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public JsonResult AdminApplyPromotionCodeForCustomerOrder(int orderId, int cartId, string promotionCode, string shippingPromotionCode, string statusOrder)
        {
            try
            {
                if (cartId == 0)
                {
                    return Json(new { success = false, message = "You don't have any product in your cart yet, please add something and try again!" }, JsonRequestBehavior.AllowGet);
                }

                if (promotionCode != "REMOVE" && !string.IsNullOrEmpty(promotionCode))
                {

                    PromotionModel promo = _service.Promotion.GetPromotionByPromotionCode(promotionCode, true);

                    if (promo == null)
                    {
                        return Json(new { success = false, message = "Your promotion code is not correct! Please Try again!" }, JsonRequestBehavior.AllowGet);
                    }

                    ShoppingCartModel applied = _service.ShoppingCart.ApplyPromotionCode(cartId, promo);
                }
                else if(!string.IsNullOrEmpty(promotionCode))
                {
                    ShoppingCartModel applied = _service.ShoppingCart.RemovePromotion(cartId);
                }

                if (shippingPromotionCode != "REMOVE" && !string.IsNullOrEmpty(shippingPromotionCode))
                {

                    PromotionModel promo = _service.Promotion.GetPromotionByPromotionCode(shippingPromotionCode, true);

                    if (promo == null)
                    {
                        return Json(new { success = false, message = "Your promotion code is not correct! Please Try again!" }, JsonRequestBehavior.AllowGet);
                    }

                    ShoppingCartModel applied = _service.ShoppingCart.ApplyPromotionCode(cartId, promo);
                }
                else if (!string.IsNullOrEmpty(shippingPromotionCode))
                {
                    ShoppingCartModel applied = _service.ShoppingCart.RemoveShippingPromotion(cartId);
                }

                if (!string.IsNullOrEmpty(statusOrder))
                {
                    _service.Order.UpdateStatusForOrder(orderId, statusOrder);
                }

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult DeleteUnusedShoppingCart()
        {
            try
            {
                bool data = _service.ShoppingCart.DeleteAllUnusedShoppingCart();

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The unused shopping carts are deleted successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot delete the unused shopping carts! There were some error in process!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }
    }
}