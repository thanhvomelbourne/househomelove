using BusinessService.IBusiness;
using BusinessService.Models;
using BusinessService.Utility;
using CoreLibrary.Data;
using CoreLibrary.Data.Entity;
using CoreLibrary.Data.Interface;
using Database.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BusinessService.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWorkFactory;
        private readonly IShoppingCartBusiness _shoppingCartBusiness;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public OrderBusiness(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory, IShoppingCartBusiness shoppingCartBusiness)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _shoppingCartBusiness = shoppingCartBusiness;
        }

        #region Implementation of IOrderBusiness

        public CollectionModel<OrderModel> GetAllOrdersByFilter(Parameter parameter)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    string search = parameter != null ? parameter.Search : null;
                    int pageNo = parameter != null ? parameter.PageNo : 1;

                    IQueryable<Order> list = !string.IsNullOrEmpty(search) ? uow.Repository<Order>().AsNoTracking().Where(x => x.Id.ToString().Contains(search) || x.FirstName.Contains(search) || x.LastName.Contains(search) || x.CompanyName.Contains(search) || x.Address.Contains(search) || x.Postcode.Contains(search) || x.Suburb.Contains(search) || x.Country.Contains(search) || x.State.Contains(search) || x.Email.Contains(search) || x.Phone.Contains(search) || x.CreatedBy.Contains(search) || x.UpdatedBy.Contains(search)) : uow.Repository<Order>().AsNoTracking();

                    if (parameter.SearchByConfirmedStatus == Constant.ConfirmedStatus.Succeed)
                    {
                        list = list.Where(x => x.ConfirmedStatus == Constant.ConfirmedStatus.Succeed);
                    }
                    else if (parameter.SearchByConfirmedStatus == Constant.ConfirmedStatus.SucceedAndConfirmed)
                    {
                        list = list.Where(x => x.ConfirmedStatus == Constant.ConfirmedStatus.SucceedAndConfirmed);
                    }
                    else if (parameter.SearchByConfirmedStatus == Constant.ConfirmedStatus.Failed)
                    {
                        list = list.Where(x => x.ConfirmedStatus == Constant.ConfirmedStatus.Failed);
                    }
                    else if (parameter.SearchByConfirmedStatus == Constant.ConfirmedStatus.Processing)
                    {
                        list = list.Where(x => x.ConfirmedStatus == Constant.ConfirmedStatus.Processing);
                    }
                    else if (parameter.SearchByConfirmedStatus == Constant.ConfirmedStatus.Paidapart)
                    {
                        list = list.Where(x => x.ConfirmedStatus == Constant.ConfirmedStatus.Paidapart);
                    }

                    if (parameter.SearchByShippingMethod == Constant.ShippingMethod.AustraliaPost)
                    {
                        list = list.Where(x => x.ShippingMethod == Constant.ShippingMethod.AustraliaPost);
                    }
                    else if (parameter.SearchByShippingMethod == Constant.ShippingMethod.ClickAndCollect)
                    {
                        list = list.Where(x => x.ShippingMethod == Constant.ShippingMethod.ClickAndCollect);
                    }
                    else if (parameter.SearchByShippingMethod == Constant.ShippingMethod.HomeMail)
                    {
                        list = list.Where(x => x.ShippingMethod == Constant.ShippingMethod.HomeMail);
                    }

                    list = list.OrderByDescending(s => s.Id);

                    int totalItems = list.Count();
                    int totalPages = (totalItems % Constant.CommonValue.PageSize20) == 0 ? (totalItems / Constant.CommonValue.PageSize20) : (totalItems / Constant.CommonValue.PageSize20) + 1;

                    list = list.Skip(Constant.CommonValue.PageSize20 * (pageNo - 1)).Take(Constant.CommonValue.PageSize20);

                    List<OrderModel> modelList = new List<OrderModel>();

                    foreach (Order c in list)
                    {
                        modelList.Add(Utility.Mapping.ConvertOrderToOrderModel(c));
                    }

                    return new CollectionModel<OrderModel>
                    {
                        Data = modelList,
                        TotalPages = totalPages,
                        TotalItems = totalItems,
                        CurrenPage = pageNo,
                    };
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public List<OrderForExportModel> GetAllOrdersForExportToExcel()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    IQueryable<Order> list = uow.Repository<Order>().AsNoTracking();

                    list = list.OrderByDescending(s => s.Id);

                    List<OrderForExportModel> modelList = new List<OrderForExportModel>();

                    foreach (Order c in list)
                    {
                        modelList.Add(Utility.Mapping.ConvertOrderToOrderForExportModel(c));
                    }

                    return modelList;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public List<OrderModel> GetHistoryTransaction(int userId)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    List<Order> orders = uow.Repository<Order>().AsNoTracking().Where(x => x.UserId == userId).OrderByDescending(x => x.Id).ToList();

                    if (orders.Any())
                    {
                        List<OrderModel> orderList = new List<OrderModel>();

                        foreach (Order order in orders)
                        {
                            orderList.Add(Utility.Mapping.ConvertOrderToOrderModel(order));
                        }

                        return orderList;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public OrderModel Checkout(CheckoutModel checkout, CheckoutPaymentModel payment)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Order insert = new Order()
                    {
                        CartId = checkout.CartId,
                        UserId = checkout.UserId,
                        FirstName = checkout.FirstName,
                        LastName = checkout.LastName,
                        CompanyName = checkout.CompanyName,
                        Address = checkout.Address,
                        Suburb = checkout.Suburb,
                        Postcode = checkout.Postcode,
                        State = checkout.State,
                        Country = checkout.Country,
                        Email = checkout.Email,
                        Phone = checkout.Phone,
                        DeliveryFirstName = checkout.DeliveryFirstName,
                        DeliveryLastName = checkout.DeliveryLastName,
                        DeliveryCompanyName = checkout.DeliveryCompanyName,
                        DeliveryAddress = checkout.DeliveryAddress,
                        DeliverySuburb = checkout.DeliverySuburb,
                        DeliveryPostcode = checkout.DeliveryPostcode,
                        DeliveryState = checkout.DeliveryState,
                        DeliveryCountry = checkout.DeliveryCountry,
                        Note = checkout.Note,
                        ConfirmedStatus = Constant.ConfirmedStatus.Processing,
                        ShippingMethod = checkout.ShippingMethod
                    };

                    Order inserted = uow.Repository<Order>().Add(insert);

                    uow.SaveChanges();

                    ShoppingCart cart = uow.Repository<ShoppingCart>().Where(x => x.Id == checkout.CartId).Include("CartItems").FirstOrDefault();

                    Payment pinsert = new Payment()
                    {
                        OrderId = inserted.Id,
                        Method = payment.Method,
                        Amount = cart.TotalFinalPrice,
                        Success = false,
                        IPAddress = Helper.ClientIpAddress
                    };

                    Payment pinserted = uow.Repository<Payment>().Add(pinsert);

                    uow.SaveChanges();

                    Order result = uow.Repository<Order>().Where(x => x.Id == insert.Id).Include("ShoppingCart").Include("Payments").FirstOrDefault();

                    OrderModel model = Utility.Mapping.ConvertOrderToOrderModel(result);

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

        public OrderModel PlaceOrder(CheckoutModel checkout)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Order insert = new Order()
                    {
                        CartId = checkout.CartId,
                        UserId = checkout.UserId,
                        FirstName = checkout.FirstName,
                        LastName = checkout.LastName,
                        CompanyName = checkout.CompanyName,
                        Address = checkout.Address,
                        Suburb = checkout.Suburb,
                        Postcode = checkout.Postcode,
                        State = checkout.State,
                        Country = checkout.Country,
                        Email = checkout.Email,
                        Phone = checkout.Phone,
                        DeliveryFirstName = checkout.DeliveryFirstName,
                        DeliveryLastName = checkout.DeliveryLastName,
                        DeliveryCompanyName = checkout.DeliveryCompanyName,
                        DeliveryAddress = checkout.DeliveryAddress,
                        DeliverySuburb = checkout.DeliverySuburb,
                        DeliveryPostcode = checkout.DeliveryPostcode,
                        DeliveryState = checkout.DeliveryState,
                        DeliveryCountry = checkout.DeliveryCountry,
                        Note = checkout.Note,
                        ConfirmedStatus = Constant.ConfirmedStatus.Processing,
                        ShippingMethod = checkout.ShippingMethod
                    };

                    Order inserted = uow.Repository<Order>().Add(insert);

                    try
                    {
                        uow.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                        throw new DataLayerException("Sorry, this order cannot be created. Please check your detail information again!");
                    }


                    if (checkout.ShippingMethod.Equals(Constant.ShippingMethod.HomeMail))
                    {
                        bool exists = Enum.IsDefined(typeof(Constant.HomeMailServicePostcode), Convert.ToInt32(checkout.DeliveryPostcode));

                        if (!exists)
                        {
                            checkout.ShippingMethod = Constant.ShippingMethod.AustraliaPost;
                            throw new DataLayerException("Sorry, our HomeMail service is not available in your area. Please accept Australia Post instead of HomeMail!");
                        }
                    }

                    Order result = uow.Repository<Order>().Where(x => x.Id == insert.Id).Include("ShoppingCart").Include("Payments").FirstOrDefault();

                    OrderModel model = Utility.Mapping.ConvertOrderToOrderModel(result);

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

        public OrderModel UpdateOrder(CheckoutModel checkout)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Order update = uow.Repository<Order>().FirstOrDefault(x => x.Id == checkout.Id);

                    update.CartId = checkout.CartId;
                    update.UserId = checkout.UserId;
                    update.FirstName = checkout.FirstName;
                    update.LastName = checkout.LastName;
                    update.CompanyName = checkout.CompanyName;
                    update.Address = checkout.Address;
                    update.Suburb = checkout.Suburb;
                    update.Postcode = checkout.Postcode;
                    update.State = checkout.State;
                    update.Country = checkout.Country;
                    update.Email = checkout.Email;
                    update.Phone = checkout.Phone;
                    update.DeliveryFirstName = checkout.DeliveryFirstName;
                    update.DeliveryLastName = checkout.DeliveryLastName;
                    update.DeliveryCompanyName = checkout.DeliveryCompanyName;
                    update.DeliveryAddress = checkout.DeliveryAddress;
                    update.DeliverySuburb = checkout.DeliverySuburb;
                    update.DeliveryPostcode = checkout.DeliveryPostcode;
                    update.DeliveryState = checkout.DeliveryState;
                    update.DeliveryCountry = checkout.DeliveryCountry;
                    update.Note = checkout.Note;
                    update.ConfirmedStatus = Constant.ConfirmedStatus.Processing;
                    update.ShippingMethod = checkout.ShippingMethod;

                    try
                    {
                        uow.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                        throw new DataLayerException("Sorry, this order cannot be updated. Please check your detail information again!");
                    }

                    if (checkout.ShippingMethod.Equals(Constant.ShippingMethod.HomeMail))
                    {
                        bool exists = Enum.IsDefined(typeof(Constant.HomeMailServicePostcode), Convert.ToInt32(checkout.DeliveryPostcode));

                        if (!exists)
                        {
                            checkout.ShippingMethod = Constant.ShippingMethod.AustraliaPost;
                            throw new DataLayerException("Sorry, our HomeMail service is not available in your area. Please accept Australia Post instead of HomeMail!");
                        }
                    }

                    OrderModel model = Utility.Mapping.ConvertOrderToOrderModel(update);

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

        public OrderModel UpdateCustomerInfo(CheckoutModel checkout)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Order update = uow.Repository<Order>().FirstOrDefault(x => x.Id == checkout.Id);

                    update.FirstName = checkout.FirstName;
                    update.LastName = checkout.LastName;
                    update.CompanyName = checkout.CompanyName;
                    update.Address = checkout.Address;
                    update.Suburb = checkout.Suburb;
                    update.Postcode = checkout.Postcode;
                    update.State = checkout.State;
                    update.Country = checkout.Country;
                    update.Email = checkout.Email;
                    update.Phone = checkout.Phone;
                    update.DeliveryFirstName = checkout.DeliveryFirstName;
                    update.DeliveryLastName = checkout.DeliveryLastName;
                    update.DeliveryCompanyName = checkout.DeliveryCompanyName;
                    update.DeliveryAddress = checkout.DeliveryAddress;
                    update.DeliverySuburb = checkout.DeliverySuburb;
                    update.DeliveryPostcode = checkout.DeliveryPostcode;
                    update.DeliveryState = checkout.DeliveryState;
                    update.DeliveryCountry = checkout.DeliveryCountry;

                    if (checkout.ShippingMethodType == (int)Constant.ShippingMethodType.AustraliaPost)
                    {
                        update.ShippingMethod = Constant.ShippingMethod.AustraliaPost;
                    }
                    else if (checkout.ShippingMethodType == (int)Constant.ShippingMethodType.HomeMail)
                    {
                        update.ShippingMethod = Constant.ShippingMethod.HomeMail;
                    }
                    else
                    {
                        update.ShippingMethod = Constant.ShippingMethod.ClickAndCollect;
                    }

                    try
                    {
                        uow.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                        throw new DataLayerException("Sorry, this order cannot be updated. Please check your detail information again!");
                    }

                    OrderModel model = Utility.Mapping.ConvertOrderToOrderModel(update);

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

        public OrderModel PayNext(CheckoutModel checkout, CheckoutPaymentModel payment)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    ShoppingCart cart = uow.Repository<ShoppingCart>().Where(x => x.Id == checkout.CartId).Include("CartItems").FirstOrDefault();

                    Payment insert = new Payment()
                    {
                        OrderId = checkout.Id,
                        Method = payment.Method,
                        Amount = cart.TotalFinalPrice,
                        Success = false,
                        IPAddress = Helper.ClientIpAddress
                    };

                    uow.Repository<Payment>().Add(insert);

                    uow.SaveChanges();

                    Order result = uow.Repository<Order>().Where(x => x.Id == checkout.Id).Include("ShoppingCart").Include("Payments").FirstOrDefault();

                    OrderModel model = Utility.Mapping.ConvertOrderToOrderModel(result);

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

        public OrderModel CompletePayment(OrderModel order, string status, string message, bool isFull = true, double amount = 0)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Order result = uow.Repository<Order>().Where(x => x.Id == order.Checkout.Id).Include("ShoppingCart").Include("Payments").FirstOrDefault();

                    Payment lastPayment = result?.Payments.OrderByDescending(x => x.Id).FirstOrDefault();

                    if (lastPayment != null)
                    {
                        lastPayment.Success = status == "approved";
                        lastPayment.Reason = status;
                        lastPayment.Detail = message;
                        lastPayment.IsFull = isFull;

                        if (!isFull)
                        {
                            lastPayment.Amount = amount;
                        }

                        if (lastPayment.Success && isFull)
                        {
                            UpdateStockLevel(result);
                            UpdateLimitTimeOfPromotion(result);
                            result.ConfirmedStatus = Constant.ConfirmedStatus.Succeed;
                            result.CompletedAt = DateTime.UtcNow;
                        }
                        else if (lastPayment.Success && !isFull && lastPayment.Method == Constant.PaymentMethod.EVoucher)
                        {
                            result.ConfirmedStatus = Constant.ConfirmedStatus.Paidapart;
                        }
                        else
                        {
                            result.ConfirmedStatus = Constant.ConfirmedStatus.Failed;
                        }
                    }

                    uow.SaveChanges();

                    OrderModel model = Utility.Mapping.ConvertOrderToOrderModel(result);

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

        public OrderModel GetOrderConfirmation(int orderId, bool reCalculate = false)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Order result = uow.Repository<Order>().Where(x => x.Id == orderId).Include("ShoppingCart").Include("Payments").FirstOrDefault();

                    if (result != null)
                    {
                        if (reCalculate)
                        {
                            result.ShoppingCart = _shoppingCartBusiness.CalculateShoppingCart(result.ShoppingCart);
                        }

                        UserProfile user = null;

                        if(result.UserId != 0)
                        {
                            user = uow.Repository<UserProfile>().Where(x => x.UserId == result.UserId).FirstOrDefault();
                        }

                        OrderModel model = Utility.Mapping.ConvertOrderToOrderModel(result, user);

                        return model;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public bool AddTrackingNumber(string trackingNumber, int orderId)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Order update = uow.Repository<Order>().FirstOrDefault(x => x.Id == orderId);

                    if (update != null)
                    {
                        update.TrackingNumber = trackingNumber;
                        update.ConfirmedStatus = Constant.ConfirmedStatus.SucceedAndConfirmed;
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

        public bool AddNote(string note, int orderId)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Order update = uow.Repository<Order>().FirstOrDefault(x => x.Id == orderId);

                    if (update != null)
                    {
                        update.Note = note;
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

        public OrderModel CheckoutByEVoucher(OrderModel order, CheckoutPaymentModel payment)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    EVoucher detail = uow.Repository<EVoucher>().AsNoTracking().FirstOrDefault(x => x.EVoucherSerialNo == payment.EVoucherSerialNo && x.ActivationCode == payment.ActivationCode);

                    if (detail == null)
                    {
                        return null;
                    }

                    if (detail.IsLive && detail.Balance > 0)
                    {
                        ShoppingCart cart = uow.Repository<ShoppingCart>().Where(x => x.Id == order.Checkout.CartId).Include("CartItems").FirstOrDefault();

                        string paymentDescription = $"EVoucherId: {detail.Id} / Serial No: {detail.EVoucherSerialNo} / Activation Code: {detail.ActivationCode}";

                        if (cart != null && detail.Balance >= cart.TotalFinalPrice)
                        {
                            detail.Balance = detail.Balance - cart.TotalFinalPrice;

                            CompletePayment(order, "approved", paymentDescription);

                            uow.SaveChanges();
                        }
                        else
                        {
                            CompletePayment(order, "approved", paymentDescription, false, detail.Balance);

                            Product evoucherTemplate = uow.Repository<Product>().AsNoTracking().FirstOrDefault(x => x.Id == Constant.CommonValue.EVoucherProductId);

                            CartItem insertItem = new CartItem()
                            {
                                CartId = order.Checkout.CartId,
                                ProductId = evoucherTemplate.Id,
                                Name = $"EVoucher {detail.EVoucherSerialNo}",
                                Description = evoucherTemplate.Description,
                                Unit = evoucherTemplate.Unit,
                                UnitOfPrice = evoucherTemplate.UnitOfPrice,
                                Quantity = 1,
                                OriginalPrice = detail.Balance * -1,
                                DiscountPrice = detail.Balance * -1,
                                FinalPrice = Math.Round(detail.Balance, 2),
                                PrimaryImage = evoucherTemplate.PrimaryImage
                            };

                            uow.Repository<CartItem>().Add(insertItem);

                            detail.Balance = 0;

                            uow.SaveChanges();
                        }

                        Order result = uow.Repository<Order>().Where(x => x.Id == order.Checkout.Id).Include("ShoppingCart").Include("Payments").FirstOrDefault();

                        OrderModel model = Utility.Mapping.ConvertOrderToOrderModel(result);

                        return model;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        private void UpdateStockLevel(Order order)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    foreach (CartItem item in order.ShoppingCart.CartItems)
                    {
                        Product p = uow.Repository<Product>().FirstOrDefault(x => x.Id == item.ProductId);

                        if (p != null)
                        {
                            p.StockLevel = p.StockLevel - item.Quantity;
                            p.InStock = p.StockLevel > 2 ? Constant.CommonValue.InStock : p.StockLevel > 0 ? Constant.CommonValue.LowStock : Constant.CommonValue.OutOfStock;

                            if (p.InStock == Constant.CommonValue.OutOfStock)
                            {
                                p.DiscountPrice = p.OriginalPrice;
                                p.DiscountRate = p.OriginalRate;
                            }
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

        private void UpdateLimitTimeOfPromotion(Order order)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    if (order.ShoppingCart.Promotion != null)
                    {
                        order.ShoppingCart.Promotion.LimitTime = (order.ShoppingCart.Promotion.LimitTime != null && order.ShoppingCart.Promotion.LimitTime > 0) ? --order.ShoppingCart.Promotion.LimitTime : order.ShoppingCart.Promotion.LimitTime;

                        if (order.ShoppingCart.Promotion.LimitTime == 0)
                        {
                            order.ShoppingCart.Promotion.IsLive = false;
                        }
                    }

                    if (order.ShoppingCart.ShippingPromotion != null)
                    {
                        order.ShoppingCart.ShippingPromotion.LimitTime = (order.ShoppingCart.ShippingPromotion.LimitTime != null && order.ShoppingCart.ShippingPromotion.LimitTime > 0) ? --order.ShoppingCart.ShippingPromotion.LimitTime : order.ShoppingCart.ShippingPromotion.LimitTime;

                        if (order.ShoppingCart.ShippingPromotion.LimitTime == 0)
                        {
                            order.ShoppingCart.Promotion.IsLive = false;
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

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Order order = uow.Repository<Order>().FirstOrDefault(x => x.Id == id);

                    if (order == null)
                    {
                        throw new DataLayerException("The order is not found in the system");
                    }
                    else
                    {
                        if (order.ShoppingCart != null)
                        {
                            if (order.ShoppingCart.CartItems.Any())
                            {
                                List<CartItemModel> deleteCartItems = Mapping.ConvertListCartItemToListCartItemModel(order.ShoppingCart.CartItems);

                                foreach (CartItemModel ci in deleteCartItems)
                                {
                                    CartItem item = uow.Repository<CartItem>().FirstOrDefault(x => x.Id == ci.Id);
                                    uow.Repository<CartItem>().Remove(item);
                                }
                            }

                            uow.Repository<ShoppingCart>().Remove(order.ShoppingCart);
                        }

                        if (order.Payments.Any())
                        {
                            List<PaymentModel> deletePayments = Mapping.ConvertListPaymentToListPaymentModel(order.Payments);

                            foreach (PaymentModel ci in deletePayments)
                            {
                                Payment item = uow.Repository<Payment>().FirstOrDefault(x => x.Id == ci.Id);
                                uow.Repository<Payment>().Remove(item);
                            }
                        }
                    }

                    uow.Repository<Order>().Remove(order);

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

        public void ForceToCompleteOrder(int orderId)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Order order = uow.Repository<Order>().AsNoTracking().FirstOrDefault(x => x.Id == orderId);

                    if (order != null)
                    {
                        order.CompletedAt = DateTime.UtcNow;
                        order.ConfirmedStatus = Constant.ConfirmedStatus.Succeed;

                        uow.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
            }
        }

        public void UpdateStatusForOrder(int orderId, string status)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Order order = uow.Repository<Order>().AsNoTracking().FirstOrDefault(x => x.Id == orderId);

                    if (order != null)
                    {
                        if (status.Equals(Constant.ConfirmedStatus.Failed) || status.Equals(Constant.ConfirmedStatus.Paidapart) || status.Equals(Constant.ConfirmedStatus.Processing))
                        {
                            order.ConfirmedStatus = status;
                            order.CompletedAt = null;
                        }
                        else
                        {
                            order.ConfirmedStatus = status;
                            order.CompletedAt = DateTime.UtcNow;
                        }

                        uow.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
            }
        }

        #endregion
    }
}
