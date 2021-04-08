using BusinessService.Models;
using BusinessService.Models.Common;
using BusinessService.Service;
using CoreLibrary.Data;
using log4net;
using OnlineStore.Helper;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using WebMatrix.WebData;

namespace OnlineStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IService _service;
        private Payment _payment;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public OrderController(IService service)
        {
            this._service = service;
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutModel model, CheckoutPaymentModel payment)
        {
            try
            {
                log.Debug($"OrderController - Line 32 - Start checkout for cart Id: {model.CartId} / Firstname: {model.FirstName}");

                model.UserId = WebSecurity.CurrentUserId != -1 ? WebSecurity.CurrentUserId : 0;

                OrderModel result = model.Id == 0 ? _service.Order.Checkout(model, payment) : _service.Order.PayNext(model, payment);

                if (result != null)
                {
                    UtilityHelper.SetCookie(UtilityHelper.ORDER_ID, result.Checkout.Id.ToString(), DateTime.UtcNow.AddDays(1));
                }
                else
                {
                    log.Info($"OrderController - Line 44 - There is some error on Checkout or PayNext method.");
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

                if (payment.Method == Constant.PaymentMethod.CreditCard)
                {
                    Payment payPal = PaymentWithCreditCard(result, payment);

                    if (payPal != null && payPal.state == "approved")
                    {
                        string message = $"Id: {payPal.id}, Card number: {payPal.payer.funding_instruments[0].credit_card.number}";

                        result = _service.Order.CompletePayment(result, payPal.state, message);

                        log.Info($"OrderController - Line 65 - {message}");

                        return Json(new { success = true, data = result, urlReturn = "" }, JsonRequestBehavior.AllowGet);
                    }

                    string errorMessage = "Failed with Credit card.";

                    _service.Order.CompletePayment(result, "failed", errorMessage);

                    log.Info($"OrderController - Line 65 - {errorMessage}");
                    return Json(new { success = false, message = errorMessage }, JsonRequestBehavior.AllowGet);
                }

                if (payment.Method == Constant.PaymentMethod.PayPal)
                {
                    string urlReturn = PaymentWithPaypal(result, payment);

                    return Json(new { success = true, data = result, urlReturn }, JsonRequestBehavior.AllowGet);
                }

                if (payment.Method == Constant.PaymentMethod.EVoucher)
                {
                    OrderModel payByVoucher = _service.Order.CheckoutByEVoucher(result, payment);

                    if (payByVoucher.PaymentList.Any(x => x.IsFull))
                    {
                        return Json(new { success = true, data = payByVoucher, urlReturn = "" }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new { success = true, data = result, urlReturn = "EVoucher" }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { success = false, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult PlaceOrder(CheckoutModel model)
        {
            try
            {
                model.UserId = WebSecurity.CurrentUserId != -1 ? WebSecurity.CurrentUserId : 0;

                OrderModel result = model.Id == 0 ? _service.Order.PlaceOrder(model) : _service.Order.UpdateOrder(model);

                if (result != null)
                {
                    UtilityHelper.SetCookie(UtilityHelper.ORDER_ID, result.Checkout.Id.ToString(), DateTime.UtcNow.AddDays(1));

                    return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = false, message = "Cannot create this order. There were some errors in the process!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult UpdateCustomerInfo(CheckoutModel model)
        {
            try
            {
                if(model.Id == 0)
                {
                    return Json(new { success = false, message = "Cannot create this order. There were some errors in the process!" }, JsonRequestBehavior.AllowGet);
                }

                OrderModel result = _service.Order.UpdateCustomerInfo(model);

                if (result != null)
                {
                    return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = false, message = "Cannot create this order. There were some errors in the process!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult AddTrackingNumber(string trackingNumber, int orderId)
        {
            try
            {
                bool result = _service.Order.AddTrackingNumber(trackingNumber, orderId);
                
                return Json(new { success = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult AddNote(string note, int orderId)
        {
            try
            {
                bool result = _service.Order.AddNote(note, orderId);
                
                return Json(new { success = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetOrderConfirmation(int orderId)
        {
            try
            {
                if (orderId == 0)
                {
                    if (Request["id"] != null)
                    {
                        orderId = Convert.ToInt32(Request["id"]);
                    }
                }

                OrderModel result = _service.Order.GetOrderConfirmation(orderId);

                if (result != null && (result.Checkout.UserId == WebSecurity.CurrentUserId || WebSecurity.CurrentUserId == -1 && result.Checkout.UserId == 0))
                {
                    UtilityHelper.SetCookie(UtilityHelper.ORDER_ID, null, DateTime.UtcNow.AddDays(-1));
                    UtilityHelper.SetCookie(UtilityHelper.SHOPPING_CART_ID, null, DateTime.UtcNow.AddDays(-1));

                    try
                    {
                        ApplicationSettingModel appConfig = _service.ApplicationSetting.AppConfiguration();
                        EmailHelper.SendConfirmationEmail(appConfig, result);
                    }
                    catch (Exception ex)
                    {
                        log.Info($"Cannot send order confirmation email: {ex.Message}");
                    }

                    return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = false, message = "You cannot get the order confirmation for this order!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetOrderByOrderId(int orderId)
        {
            try
            {
                OrderModel result = _service.Order.GetOrderConfirmation(orderId);

                if (result != null && (result.Checkout.UserId == WebSecurity.CurrentUserId || WebSecurity.CurrentUserId == -1 && result.Checkout.UserId == 0))
                {
                    return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult GetOrderByOrderIdForAdmin(int orderId, bool reCalculate = false)
        {
            try
            {
                OrderModel result = _service.Order.GetOrderConfirmation(orderId, reCalculate);

                return result != null ? Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet) : Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private Payment PaymentWithCreditCard(OrderModel order, CheckoutPaymentModel payment)
        {
            List<Item> itms = new List<Item>();

            if (order != null)
            {
                foreach (CartItemModel it in order.ShoppingCart.CartItemList)
                {
                    itms.Add(new Item { name = it.Name, currency = it.UnitOfPrice, price = Math.Round(it.DiscountPrice, 2).ToString("F"), quantity = it.Quantity.ToString() });
                }

                if (order.ShoppingCart.PromotionId != 0 && order.ShoppingCart.Promotion != null)
                {
                    itms.Add(new Item { name = order.ShoppingCart.Promotion.NameOfPromotion, currency = "AUD", price = Math.Round(order.ShoppingCart.Discount * -1, 2).ToString("F"), quantity = "1" });
                }
            }

            ItemList itemList = new ItemList()
            {
                items = itms
            };

            //Address for the payment
            Address billingAddress = new Address
            {
                city = !string.IsNullOrEmpty(order.Checkout.Suburb) ? order.Checkout.Suburb : "N/A",
                country_code = "AU",
                line1 = !string.IsNullOrEmpty(order.Checkout.Address) ? order.Checkout.Address : "N/A",
                postal_code = !string.IsNullOrEmpty(order.Checkout.Postcode) ? order.Checkout.Postcode : "0000",
                state = !string.IsNullOrEmpty(order.Checkout.State) ? order.Checkout.State : "N/A",
                phone = order.Checkout.Phone
            };

            //Now Create an object of credit card and add above details to it
            //Please replace your credit card details over here which you got from paypal
            CreditCard crdtCard = new CreditCard
            {
                billing_address = billingAddress,
                cvv2 = payment.CCV,
                expire_month = payment.MonthExpire,
                expire_year = payment.YearExpire,
                first_name = payment.CardFirstName,
                last_name = payment.CardLastName,
                number = payment.CardNumber,
                type = payment.CreditCardType
            };

            // Specify details of your payment amount.
            Details details = new Details
            {
                shipping = Math.Round(order.ShoppingCart.ShippingFee, 2).ToString("F"),
                subtotal = Math.Round(order.ShoppingCart.TotalAmountExGst - order.ShoppingCart.Discount, 2)
                    .ToString("F"),
                tax = Math.Round(order.ShoppingCart.Gst, 2).ToString("F")
            };

            // Specify your total payment amount and assign the details object
            Amount amnt = new Amount
            {
                currency = "AUD",
                total = Math.Round(order.ShoppingCart.TotalFinalPrice, 2).ToString("F"),
                details = details
            };

            // Now make a transaction object and assign the Amount object
            Transaction tran = new Transaction
            {
                amount = amnt,
                description = "Payment for order " + order.Checkout.Id,
                item_list = itemList,
                invoice_number = order.PaymentList.OrderByDescending(x => x.Id).FirstOrDefault()?.Id.ToString()
            };

            // Now, we have to make a list of transaction and add the transactions object
            // to this list. You can create one or more object as per your requirements

            List<Transaction> transactions = new List<Transaction> { tran };

            // Now we need to specify the FundingInstrument of the Payer
            // for credit card payments, set the CreditCard which we made above

            FundingInstrument fundInstrument = new FundingInstrument { credit_card = crdtCard };

            // The Payment creation API requires a list of FundingIntrument

            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument> { fundInstrument };

            // Now create Payer object and assign the fundinginstrument list to the object
            Payer payr = new Payer
            {
                funding_instruments = fundingInstrumentList,
                payment_method = "credit_card"
            };

            // finally create the payment object and assign the payer object & transaction list to it
            Payment pymnt = new Payment
            {
                intent = "sale",
                payer = payr,
                transactions = transactions
            };

            try
            {
                APIContext apiContext = ConfigurationHelper.GetApiContext();

                Payment createdPayment = pymnt.Create(apiContext);

                if (createdPayment.state.ToLower() == "approved")
                {
                    return createdPayment;
                }

                return null;
            }
            catch (PayPal.PayPalException ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return null;
            }
        }

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            try
            {
                PaymentExecution paymentExecution = new PaymentExecution() { payer_id = payerId };
                _payment = new Payment() { id = paymentId };
                return _payment.Execute(apiContext, paymentExecution);
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return null;
            }
        }

        private Payment CreatePayment(APIContext apiContext, string returnUrl, OrderModel order, CheckoutPaymentModel payment)
        {
            try
            {
                List<Item> itms = new List<Item>();

                if (order != null)
                {
                    foreach (CartItemModel it in order.ShoppingCart.CartItemList)
                    {
                        itms.Add(new Item() { name = it.Name, currency = it.UnitOfPrice, price = Math.Round(it.DiscountPrice, 2).ToString("F"), quantity = it.Quantity.ToString() });
                    }

                    if (order.ShoppingCart.PromotionId != 0 && order.ShoppingCart.Promotion != null)
                    {
                        itms.Add(new Item() { name = order.ShoppingCart.Promotion.NameOfPromotion, currency = "AUD", price = Math.Round(order.ShoppingCart.Discount * -1, 2).ToString("F"), quantity = "1" });
                    }
                }

                ItemList itemList = new ItemList { items = itms };

                Payer payer = new Payer() { payment_method = "paypal" };

                // Configure Redirect Urls here with RedirectUrls object
                RedirectUrls redirUrls = new RedirectUrls()
                {
                    cancel_url = returnUrl,
                    return_url = returnUrl
                };

                // similar as we did for credit card, do here and create details object
                Details details = new Details
                {
                    shipping = Math.Round(order.ShoppingCart.ShippingFee, 2).ToString("F"),
                    subtotal = Math.Round(order.ShoppingCart.TotalAmountExGst - order.ShoppingCart.Discount, 2)
                        .ToString("F"),
                    tax = Math.Round(order.ShoppingCart.Gst, 2).ToString("F")
                };

                // similar as we did for credit card, do here and create amount object
                Amount amnt = new Amount
                {
                    currency = "AUD",
                    total = Math.Round(order.ShoppingCart.TotalFinalPrice, 2).ToString("F"),
                    details = details
                };

                Transaction tran = new Transaction
                {
                    amount = amnt,
                    description = "Payment for order " + order.Checkout.Id,
                    item_list = itemList,
                    invoice_number = order.PaymentList.OrderByDescending(x => x.Id).FirstOrDefault()?.Id.ToString()
                };

                List<Transaction> transactions = new List<Transaction>
            {
                tran
            };

                Payment pymnt = new Payment
                {
                    intent = "sale",
                    payer = payer,
                    transactions = transactions,
                    redirect_urls = redirUrls
                };

                // Create a payment using a APIContext
                return pymnt.Create(apiContext);
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return null;
            }
        }

        public string PaymentWithPaypal(OrderModel order, CheckoutPaymentModel payment)
        {
            try
            {
                //getting the apiContext as earlier
                APIContext apiContext = ConfigurationHelper.GetApiContext();

                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    // So we have provided URL of this controller only
                    string baseUri = Request.Url.Scheme + "://" + Request.Url.Authority + "/Order/VerifyPayPal?";

                    //guid we are generating for storing the paymentID received in session
                    //after calling the create function and it is used in the payment execution

                    string guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment

                    Payment createdPayment = CreatePayment(apiContext, baseUri + "guid=" + guid, order, payment);

                    //get links returned from paypal in response to Create function call

                    List<Links>.Enumerator links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);

                    return paypalRedirectUrl;
                }
                else
                {
                    // This section is executed when we have received all the payments parameters
                    // from the previous call to the function Create
                    // Executing a payment

                    string guid = Request.Params["guid"];

                    Payment executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return null;
            }

            return "/shop/checkout/orderconfirmation?id=" + order.Checkout.Id;
        }

        public ActionResult VerifyPayPal()
        {
            //getting the apiContext as earlier
            APIContext apiContext = ConfigurationHelper.GetApiContext();
            string message = string.Empty;
            int orderId = Convert.ToInt32(UtilityHelper.GetCookie(UtilityHelper.ORDER_ID, true));
            OrderModel result = _service.Order.GetOrderConfirmation(orderId);

            try
            {
                string payerId = Request.Params["PayerID"];

                if (!string.IsNullOrEmpty(payerId))
                {
                    // This section is executed when we have received all the payments parameters
                    // from the previous call to the function Create
                    // Executing a payment

                    string guid = Request.Params["guid"];

                    Payment executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    message = $"Payer Id: {payerId}, Guid: {guid}";

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        _service.Order.CompletePayment(result, "failed", message + ", Failure Reason: " + executedPayment.failure_reason);
                        return RedirectToAction("PayPalReturn", "Client");
                    }
                }
                else
                {
                    _service.Order.CompletePayment(result, "failed", "User canceled transaction!");
                    return RedirectToAction("PayPalReturn", "Client");
                }
            }
            catch (Exception ex)
            {
                log.Debug($"OrderId: {orderId} - {ex.Message} - {ex}");
                _service.Order.CompletePayment(result, "failed", "There is some errors in paypal process!");
                return RedirectToAction("PayPalReturn", "Client");
            }

            _service.Order.CompletePayment(result, "approved", message);

            return Redirect("/shop/checkout/orderconfirmation?id=" + orderId);
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Delete(int? orderIdForDelete)
        {
            try
            {
                if (orderIdForDelete == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var id = orderIdForDelete.Value;

                _service.Order.Delete(id);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The order is deleted successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot delete the order!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult ForceToCompleteOrder(int orderId)
        {
            try
            {
                _service.Order.ForceToCompleteOrder(orderId);
                
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
        public ActionResult SendInvoiceEmail(int orderId, string specificEmail)
        {
            try
            {
                string[] emails = null;

                if (specificEmail.Contains(","))
                {
                    emails = specificEmail.Split(',');
                }

                if (specificEmail.Contains(";"))
                {
                    emails = specificEmail.Split(';');
                }

                if(!string.IsNullOrEmpty(specificEmail) && !specificEmail.Contains(";") && !specificEmail.Contains(","))
                {
                    emails = new string[1];
                    emails[0] = specificEmail;
                }

                OrderModel result = _service.Order.GetOrderConfirmation(orderId);

                if (result != null)
                {
                    try
                    {
                        ApplicationSettingModel appConfig = _service.ApplicationSetting.AppConfiguration();
                        EmailHelper.SendConfirmationEmail(appConfig, result, emails);
                    }
                    catch (Exception ex)
                    {
                        log.Info($"Cannot send order confirmation email: {ex.Message} - {ex}");
                    }

                    return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult ExportToExcel(Parameter parameter)
        {
            try
            {
                var data = _service.Order.GetAllOrdersForExportToExcel();

                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = data;
                grid.DataBind();

                Response.ClearContent();
                Response.AddHeader($"content-disposition", "attachement; filename=Orders.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grid.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
                return View();
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public JsonResult GetAllShippingMethods()
        {
            List<EnumModel> enums = ((Constant.ShippingMethodType[])Enum.GetValues(typeof(Constant.ShippingMethodType))).Select(c => new EnumModel() { Value = (int)c, Name = c.GetDescriptionTextForCurrentEnum() }).ToList();

            return Json(new { success = true, data = enums }, JsonRequestBehavior.AllowGet);
        }
    }
}