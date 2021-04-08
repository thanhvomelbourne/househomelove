using BusinessService.IBusiness;
using BusinessService.Models;
using BusinessService.Models.Common;
using BusinessService.Utility;
using CoreLibrary.Data;
using CoreLibrary.Data.Entity;
using CoreLibrary.Data.Interface;
using Database.Model;
using log4net;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessService.Business
{
    public class ShoppingCartBusiness : IShoppingCartBusiness
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWorkFactory;
        private readonly IApplicationSettingBusiness _applicationBusiness;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ShoppingCartBusiness(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory, IApplicationSettingBusiness applicationBusiness)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _applicationBusiness = applicationBusiness;
        }

        public ShoppingCartModel AddToCart(AddItemToShoppingCartModel param, bool isAdmin = false)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    ApplicationSetting appSetting = uow.Repository<ApplicationSetting>().AsNoTracking().FirstOrDefault();
                    Product p = uow.Repository<Product>().AsNoTracking().FirstOrDefault(x => x.Id == param.ProductId);

                    if (p == null)
                    {
                        return null;
                    }

                    if (p.StockLevel == 0)
                    {
                        throw new DataLayerException("We don't have this item in our stock, please pre-order!");
                    }

                    if (p.StockLevel - param.Quantity < 0)
                    {
                        throw new DataLayerException("We don't have enough in our stock, only " + p.StockLevel + " can be ordered!");
                    }

                    if (param.Quantity <= 0)
                    {
                        throw new DataLayerException("Number of item cannot be negative. Please add at least 1 item!");
                    }

                    if (param.CartId == 0)
                    {
                        ShoppingCart insert = new ShoppingCart()
                        {
                            TotalAmountExGst = 0,
                            TotalAmountIncGst = 0,
                            Discount = 0,
                            Gst = 0,
                            TotalFinalPrice = 0,
                            GSTPercent = appSetting.GSTPercent,
                            CreditCardSurcharge = appSetting.CreditCardSurcharge,
                            PayPalSurcharge = appSetting.PaypalSurcharge,
                            FreeShippingAusPostFrom = appSetting.FreeShippingAusPostFrom,
                            FreeShippingHomeMailFrom = appSetting.FreeShippingHomeMailFrom,
                            HomeMailDefaultPrice1 = appSetting.HomeMailDefaultPrice1,
                            HomeMailDefaultPrice2 = appSetting.HomeMailDefaultPrice2,
                            HomeMailDefaultPrice3 = appSetting.HomeMailDefaultPrice3,
                            HomeMailAvailableFrom = appSetting.HomeMailAvailableFrom,
                            WarehousePostcode = appSetting.WarehousePostcode
                        };

                        ShoppingCart inserted = uow.Repository<ShoppingCart>().Add(insert);

                        uow.SaveChanges();

                        CartItem insertItem = new CartItem()
                        {
                            CartId = inserted.Id,
                            ProductId = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Unit = p.Unit,
                            UnitOfPrice = p.UnitOfPrice,
                            Quantity = param.Quantity,
                            OriginalPrice = p.OriginalPrice,
                            DiscountPrice = param.CustomPrice != 0 ? param.CustomPrice : p.DiscountPrice,
                            FinalPrice = Math.Round(param.CustomPrice != 0 ? param.CustomPrice : p.DiscountPrice * param.Quantity, 2),
                            PrimaryImage = p.PrimaryImage
                        };

                        CartItem insertedItem = uow.Repository<CartItem>().Add(insertItem);

                        uow.SaveChanges();

                        ShoppingCart result = uow.Repository<ShoppingCart>().Where(x => x.Id == inserted.Id).Include("CartItems").Include("Promotion").FirstOrDefault();

                        result.CartItems.OrderByDescending(x => x.CartId);

                        CalculateShoppingCart(result);

                        uow.SaveChanges();

                        ShoppingCartModel model = Utility.Mapping.ConvertShoppingCartToShoppingCartModel(result);

                        return model;
                    }
                    else
                    {
                        CartItem item = uow.Repository<CartItem>().AsNoTracking().FirstOrDefault(x => x.CartId == param.CartId && x.ProductId == param.ProductId);

                        if (item != null)
                        {
                            item.Quantity += param.Quantity;

                            if (p.StockLevel - item.Quantity < 0 && !isAdmin)
                            {
                                throw new DataLayerException("We don't have enough in our stock, only " + p.StockLevel + " can be ordered!");
                            }
                        }
                        else
                        {
                            CartItem insertItem = new CartItem()
                            {
                                CartId = param.CartId,
                                ProductId = p.Id,
                                Name = p.Name,
                                Description = p.Description,
                                Unit = p.Unit,
                                UnitOfPrice = p.UnitOfPrice,
                                Quantity = param.Quantity,
                                OriginalPrice = p.OriginalPrice,
                                DiscountPrice = param.CustomPrice != 0 ? param.CustomPrice : p.DiscountPrice,
                                FinalPrice = Math.Round(param.CustomPrice != 0 ? param.CustomPrice : p.DiscountPrice * param.Quantity, 2),
                                PrimaryImage = p.PrimaryImage
                            };

                            CartItem insertedItem = uow.Repository<CartItem>().Add(insertItem);
                        }

                        uow.SaveChanges();

                        ShoppingCart result = uow.Repository<ShoppingCart>().Where(x => x.Id == param.CartId).Include("CartItems").Include("Promotion").FirstOrDefault();

                        result.CartItems.OrderByDescending(x => x.CartId);

                        CalculateShoppingCart(result);

                        if (isAdmin)
                        {
                            if (param.ProductId != 0 && param.Quantity != 0)
                            {
                                UpdateStockLevel(param.ProductId, param.Quantity);
                            }
                        }

                        uow.SaveChanges();

                        ShoppingCartModel model = Utility.Mapping.ConvertShoppingCartToShoppingCartModel(result);

                        return model;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public ShoppingCartModel GetCurrentCart(int cartId)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    ShoppingCart result = uow.Repository<ShoppingCart>().Where(x => x.Id == cartId).Include("CartItems").Include("Promotion").FirstOrDefault();

                    if (result == null)
                    {
                        return null;
                    }

                    ApplicationSetting appSetting = uow.Repository<ApplicationSetting>().AsNoTracking().FirstOrDefault();

                    if (appSetting != null)
                    {
                        result.GSTPercent = appSetting.GSTPercent;
                        result.CreditCardSurcharge = appSetting.CreditCardSurcharge;
                        result.PayPalSurcharge = appSetting.PaypalSurcharge;
                        result.FreeShippingAusPostFrom = appSetting.FreeShippingAusPostFrom;
                        result.FreeShippingHomeMailFrom = appSetting.FreeShippingHomeMailFrom;
                        result.HomeMailDefaultPrice1 = appSetting.HomeMailDefaultPrice1;
                        result.HomeMailDefaultPrice2 = appSetting.HomeMailDefaultPrice2;
                        result.HomeMailDefaultPrice3 = appSetting.HomeMailDefaultPrice3;
                        result.HomeMailAvailableFrom = appSetting.HomeMailAvailableFrom;
                        result.WarehousePostcode = appSetting.WarehousePostcode;

                        uow.SaveChanges();
                    }

                    result.CartItems.OrderBy(x => x.CartId);

                    CalculateShoppingCart(result);

                    uow.SaveChanges();

                    ShoppingCartModel model = Utility.Mapping.ConvertShoppingCartToShoppingCartModel(result);

                    return model;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public ShoppingCartModel UpdateCurrentCart(AddItemToShoppingCartModel param)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Product p = uow.Repository<Product>().AsNoTracking().FirstOrDefault(x => x.Id == param.ProductId);

                    if (p == null)
                    {
                        return null;
                    }

                    CartItem item = uow.Repository<CartItem>().AsNoTracking().FirstOrDefault(x => x.CartId == param.CartId && x.ProductId == param.ProductId);

                    if (item != null)
                    {
                        if (param.Quantity > 0)
                        {
                            if (p.StockLevel - (item.Quantity + param.Quantity) < 0)
                            {
                                throw new DataLayerException("We don't have enough in our stock, only " + p.StockLevel + " can be ordered!");
                            }

                            item.Quantity += param.Quantity;
                        }
                        else
                        {
                            uow.Repository<CartItem>().Remove(item);
                        }
                    }

                    uow.SaveChanges();

                    ShoppingCart result = uow.Repository<ShoppingCart>().Where(x => x.Id == param.CartId).Include("CartItems").Include("Promotion").FirstOrDefault();

                    result.CartItems.OrderByDescending(x => x.CartId);

                    CalculateShoppingCart(result);

                    uow.SaveChanges();

                    ShoppingCartModel model = Utility.Mapping.ConvertShoppingCartToShoppingCartModel(result);

                    return model;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public ShoppingCartModel UpdateCurrentCartForAdmin(AddItemToShoppingCartModel param)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Product p = uow.Repository<Product>().AsNoTracking().FirstOrDefault(x => x.Id == param.ProductId);

                    if (p == null)
                    {
                        return null;
                    }

                    CartItem item = uow.Repository<CartItem>().AsNoTracking().FirstOrDefault(x => x.CartId == param.CartId && x.ProductId == param.ProductId);

                    if (item != null)
                    {
                        if (param.Quantity > 0)
                        {
                            if (p.StockLevel - (item.Quantity + param.Quantity) < 0)
                            {
                                throw new DataLayerException("We don't have enough in our stock, only " + p.StockLevel + " can be ordered!");
                            }

                            item.Quantity += param.Quantity;
                        }
                        else
                        {
                            uow.Repository<CartItem>().Remove(item);

                            p.StockLevel = p.StockLevel + 1;
                            p.InStock = p.StockLevel > 2 ? Constant.CommonValue.InStock : p.StockLevel > 0 ? Constant.CommonValue.LowStock : Constant.CommonValue.OutOfStock;
                        }
                    }

                    uow.SaveChanges();

                    ShoppingCart result = uow.Repository<ShoppingCart>().Where(x => x.Id == param.CartId).Include("CartItems").Include("Promotion").FirstOrDefault();

                    result.CartItems.OrderByDescending(x => x.CartId);

                    CalculateShoppingCart(result);

                    uow.SaveChanges();

                    ShoppingCartModel model = Utility.Mapping.ConvertShoppingCartToShoppingCartModel(result);

                    return model;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public ShoppingCartModel ApplyPromotionCode(int cartId, PromotionModel promo)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    ShoppingCart item = uow.Repository<ShoppingCart>().AsNoTracking().FirstOrDefault(x => x.Id == cartId);

                    if (item.TotalAmountExGst <= 0)
                    {
                        throw new DataLayerException("You cannot apply any discount when your subtotal is $0.");
                    }

                    if (item != null)
                    {
                        if (promo.PromotionType == Constant.PromotionType.DiscountShipping.GetHashCode())
                        {
                            item.ShippingPromotionId = promo.Id;
                        }
                        else
                        {
                            item.PromotionId = promo.Id;
                        }
                    }

                    uow.SaveChanges();

                    ShoppingCart result = uow.Repository<ShoppingCart>().Where(x => x.Id == cartId).Include("CartItems").Include("Promotion").FirstOrDefault();

                    result.CartItems.OrderByDescending(x => x.CartId);

                    CalculateShoppingCart(result);

                    uow.SaveChanges();

                    ShoppingCartModel model = Utility.Mapping.ConvertShoppingCartToShoppingCartModel(result);

                    return model;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public ShoppingCartModel RemovePromotion(int cartId)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    ShoppingCart item = uow.Repository<ShoppingCart>().AsNoTracking().FirstOrDefault(x => x.Id == cartId);

                    if (item != null)
                    {
                        item.PromotionId = null;
                    }

                    uow.SaveChanges();

                    ShoppingCart result = uow.Repository<ShoppingCart>().Where(x => x.Id == cartId).Include("CartItems").FirstOrDefault();

                    result.CartItems.OrderByDescending(x => x.CartId);

                    CalculateShoppingCart(result);

                    uow.SaveChanges();

                    ShoppingCartModel model = Utility.Mapping.ConvertShoppingCartToShoppingCartModel(result);

                    return model;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public ShoppingCartModel RemoveShippingPromotion(int cartId)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    ShoppingCart item = uow.Repository<ShoppingCart>().AsNoTracking().FirstOrDefault(x => x.Id == cartId);

                    if (item != null)
                    {
                        item.ShippingPromotionId = null;
                    }

                    uow.SaveChanges();

                    ShoppingCart result = uow.Repository<ShoppingCart>().Where(x => x.Id == cartId).Include("CartItems").FirstOrDefault();

                    result.CartItems.OrderByDescending(x => x.CartId);

                    CalculateShoppingCart(result);

                    uow.SaveChanges();

                    ShoppingCartModel model = Utility.Mapping.ConvertShoppingCartToShoppingCartModel(result);

                    return model;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public ShoppingCart CalculateShoppingCart(ShoppingCart cart)
        {
            try
            {
                if (cart == null)
                {
                    return null;
                }

                cart.ShippingFee = 0;
                cart.TotalAmountExGst = 0;
                cart.TotalAmountIncGst = 0;
                cart.TotalFinalPrice = 0;
                cart.Gst = 0;

                if (!cart.CartItems.Any())
                {
                    return cart;
                }

                ApplicationSettingModel appConfig = _applicationBusiness.AppConfiguration();

                Order currentOrder = cart.Orders.FirstOrDefault();
                string tempEstimate = "0";
                double tempShippingFee = 0;

                foreach (CartItem item in cart.CartItems)
                {
                    item.FinalPrice = Math.Round(item.Quantity * item.DiscountPrice, 2);
                    cart.TotalAmountExGst += item.FinalPrice;

                    CalculateShippingFeeForEachItem(cart, currentOrder, item.Product.Weight, item.Product.Height, item.Product.Width, item.Product.Length);

                    int oldDel = Convert.ToInt32(Regex.Match(tempEstimate, @"\d+").Value);
                    int newDel = cart.EstimateDelivery != null ? Convert.ToInt32(Regex.Match(cart.EstimateDelivery, @"\d+").Value) : Constant.CommonValue.DefaultEstimatedShipping;

                    if (newDel > oldDel)
                    {
                        tempEstimate = cart.EstimateDelivery;
                    }

                    tempShippingFee += cart.ShippingFee;
                }

                if (currentOrder == null || currentOrder.ShippingMethod.Equals(Constant.ShippingMethod.AustraliaPost))
                {
                    cart.ShippingFee = Math.Round(tempShippingFee / cart.CartItems.Count + 3.95, 2);
                }
                else if (currentOrder.ShippingMethod.Equals(Constant.ShippingMethod.ClickAndCollect))
                {
                    cart.ShippingFee = 0;
                }
                else
                {
                    cart.ShippingFee = Math.Round(appConfig.HomeMailDefaultPrice1, 2);
                }

                cart.TotalAmountExGst = Math.Round(cart.TotalAmountExGst, 2);
                cart.Gst = Math.Round(cart.TotalAmountExGst / 100 * appConfig.GSTPercent, 2);
                cart.TotalAmountIncGst = Math.Round(cart.TotalAmountExGst + cart.Gst, 2);

                if (cart.PromotionId != 0 && cart.Promotion != null)
                {
                    switch (cart.Promotion.PromotionType)
                    {
                        case Constant.PromotionTypeId.DiscountTotalOnDollars:
                            cart.Discount = Math.Round(cart.Promotion.Dollars.Value, 2);
                            break;
                        case Constant.PromotionTypeId.DiscountTotalOnPercentage:
                            cart.Discount = Math.Round(cart.TotalAmountIncGst / 100 * cart.Promotion.Percentage.Value, 2);
                            break;
                    }
                }
                else
                {
                    cart.Discount = 0;
                }

                if (cart.ShippingPromotionId != 0 && cart.ShippingPromotion != null)
                {
                    cart.ShippingFee = cart.ShippingFee - Math.Round(cart.ShippingFee * cart.ShippingPromotion.Percentage.Value / 100, 2);
                }

                if (currentOrder != null && currentOrder.ShippingMethod.Equals(Constant.ShippingMethod.HomeMail))
                {
                    if (appConfig.FreeShippingHomeMailFrom != null && appConfig.FreeShippingHomeMailFrom > 0 && cart.TotalAmountIncGst - cart.Discount >= appConfig.FreeShippingHomeMailFrom)
                    {
                        cart.ShippingFee = 0;
                    }
                }
                else
                {
                    if (appConfig.FreeShippingAusPostFrom != null && appConfig.FreeShippingAusPostFrom > 0 && cart.TotalAmountIncGst - cart.Discount >= appConfig.FreeShippingAusPostFrom)
                    {
                        cart.ShippingFee = 0;
                    }
                }

                cart.TotalFinalPrice = Math.Round(cart.TotalAmountIncGst - cart.Discount + cart.ShippingFee, 2);

                return cart;
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        private void CalculateShippingFeeForEachItem(ShoppingCart cart, Order currentOrder, double totalWeight, double totalHeight, double totalWidth, double totalLength)
        {
            try
            {
                string shipping = string.Empty;

                if (currentOrder != null && Math.Abs(totalWeight) > 0 && Math.Abs(totalHeight) > 0 && Math.Abs(totalWidth) > 0 && Math.Abs(totalLength) > 0)
                {
                    if (!string.IsNullOrEmpty(currentOrder.DeliveryPostcode))
                    {
                        shipping = Helper.AustraliaPostEngine.CalculateTotalDelivery(cart, currentOrder.DeliveryPostcode, totalLength, totalWidth, totalHeight, totalWeight);
                    }
                    else
                    {
                        shipping = Helper.AustraliaPostEngine.CalculateTotalDelivery(cart, !string.IsNullOrEmpty(currentOrder.Postcode) ? currentOrder.Postcode : Constant.AustraliaPost.DefaultShippingTo, totalLength, totalWidth, totalHeight, totalWeight);
                    }
                }
                else if (Math.Abs(totalWeight) > 0 && Math.Abs(totalHeight) > 0 && Math.Abs(totalWidth) > 0 && Math.Abs(totalLength) > 0)
                {
                    shipping = Helper.AustraliaPostEngine.CalculateTotalDelivery(cart, Constant.AustraliaPost.DefaultShippingTo, totalLength, totalWidth, totalHeight, totalWeight);
                }

                if (!string.IsNullOrEmpty(shipping) && !shipping.Contains(Constant.AustraliaPost.CallErrorMessage))
                {
                    AustraliaPostModel postage_result = new AustraliaPostModel();

                    postage_result = JsonConvert.DeserializeObject<AustraliaPostModel>(shipping);

                    cart.ShippingFee = Math.Round((Convert.ToDouble(postage_result.postage_result.total_cost) + 3), 2);
                    cart.EstimateDelivery = postage_result.postage_result.delivery_time;
                }
                else if (!string.IsNullOrEmpty(shipping) && shipping.Equals(Constant.AustraliaPost.CallErrorMessage))
                {
                    cart.ShippingFee = Constant.AustraliaPost.DefaultShippingFee;
                    cart.EstimateDelivery = Constant.AustraliaPost.DefaultEstimateDelivery;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataException(ex.Message);
            }
        }

        private void UpdateStockLevel(int productId, int reduceQuantity)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Product p = uow.Repository<Product>().FirstOrDefault(x => x.Id == productId);

                    if (p != null)
                    {
                        p.StockLevel = p.StockLevel - reduceQuantity;
                        p.InStock = p.StockLevel > 2 ? Constant.CommonValue.InStock : p.StockLevel > 0 ? Constant.CommonValue.LowStock : Constant.CommonValue.OutOfStock;

                        if (p.InStock == Constant.CommonValue.OutOfStock)
                        {
                            p.DiscountPrice = p.OriginalPrice;
                            p.DiscountRate = p.OriginalRate;
                        }
                    }

                    uow.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public bool DeleteAllUnusedShoppingCart()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    DateTime yesterday = DateTime.UtcNow.AddDays(-1);
                    var carts = uow.Repository<ShoppingCart>().Where(x => !x.Orders.Any() && x.CreatedAt <= yesterday);

                    if (carts == null)
                    {
                        return true;
                    }

                    foreach(var cart in carts.ToList())
                    {
                        foreach(var item in cart.CartItems.ToList())
                        {
                            uow.Repository<CartItem>().Remove(item);
                        }

                        uow.Repository<ShoppingCart>().Remove(cart);
                    }
                    
                    uow.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }
    }
}
