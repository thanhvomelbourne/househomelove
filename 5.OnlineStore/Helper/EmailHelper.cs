using BusinessService.Models;
using CoreLibrary.Data;
using log4net;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace OnlineStore.Helper
{
    public class EmailHelper
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string AppPath
        {
            get
            {
                try
                {
                    string path = HttpContext.Current.Request.ApplicationPath;
                    return path.EndsWith("/") ? path : path + "/";
                }
                catch
                {
                    return "???";
                }
            }
        }

        private static string AppPathUrl
        {
            get
            {
                try
                {
                    string host = HttpContext.Current.Request.Url.Scheme + "://" +
                                  (HttpContext.Current.Request.Url.IsDefaultPort ? HttpContext.Current.Request.Url.Host : HttpContext.Current.Request.Url.Authority);
                    return host + AppPath;
                }
                catch
                {
                    return "???" + AppPath;
                }
            }
        }

        private static string FixPath(string path)
        {
            if (path.StartsWith("~/"))
            {
                return AppPath + path.Substring(2);
            }

            if (path.StartsWith("/"))
            {
                return AppPath + path.Substring(1);
            }
            else
            {
                return path;
            }
        }

        private static string GetEmailTemplate(string type)
        {
            WebRequest request = null;

            if (type.Equals(Constant.EmailType.ConfirmationEmail))
            {
                request = WebRequest.Create(Constant.EmailType.ConfirmationEmailTemplate);
            }

            if (type.Equals(Constant.EmailType.RecoveryPasswordEmail))
            {
                request = WebRequest.Create(Constant.EmailType.RecoveryPasswordEmailTemplate);
            }

            if (type.Equals(Constant.EmailType.RegisterAccountEmail))
            {
                request = WebRequest.Create(Constant.EmailType.RegistrationEmailTemplate);
            }

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string template = reader.ReadToEnd();

            template = Regex.Replace(template, @"( (?:href|src)=[""'])~/?(\S*?[""'])", "$1" + AppPathUrl + "$2");

            return template;
        }

        public static bool SendConfirmationEmail(ApplicationSettingModel appConfig, OrderModel model, string[] emails = null)
        {
            string body = GetEmailTemplate(Constant.EmailType.ConfirmationEmail);
            string senderEmail = ConfigurationManager.AppSettings["EmailUsername"];
            string password = ConfigurationManager.AppSettings["EmailPassword"];

            if (!string.IsNullOrEmpty(body) || !string.IsNullOrEmpty(senderEmail))
            {
                body = body.Replace("##FIRSTNAME##", $"{model.Checkout.FirstName} {model.Checkout.LastName}");
                body = body.Replace("##ORDER_NUMBER##", model.Checkout.Id.ToString());
                body = body.Replace("##ORDER_DATE##", model.Checkout.CompletedAt.ToString());
                body = body.Replace("##BILLING_DETAIL##",
                    $"{model.Checkout.Address}, {model.Checkout.Suburb}, {model.Checkout.State} {model.Checkout.Postcode}, {model.Checkout.Country}");
                body = body.Replace("##PHONE##", model.Checkout.Phone);
                body = body.Replace("##EMAIL##", model.Checkout.Email);
                body = body.Replace("##NOTE##", !string.IsNullOrEmpty(model.Checkout.Note) ? $"Note: {model.Checkout.Note}" : string.Empty);
                body = body.Replace("##PAID_BY##", model.PaymentList != null && model.PaymentList.Any() ? model.PaymentList.Where(x => x.Success == true).Last().Method : "PAID BY CASH");
                body = body.Replace("##SUBTOTAL##", "$" + model.ShoppingCart.TotalAmountExGst.ToString());
                body = body.Replace("##GST##", "$" + model.ShoppingCart.Gst.ToString());
                body = body.Replace("##SHIPPING##", "$" + model.ShoppingCart.ShippingFee.ToString());
                body = body.Replace("##ESTIMATE_DELIVERY##", model.ShoppingCart.EstimateDelivery.ToString());
                body = body.Replace("##TOTAL##", "$" + model.ShoppingCart.TotalFinalPrice.ToString());
                body = body.Replace("##ConfirmedStatus##", model.Checkout.ConfirmedStatus);

                if(model.Checkout.ShippingMethod == Constant.ShippingMethod.ClickAndCollect)
                {
                    body = body.Replace("##DELIVERY_DETAIL##", string.Empty);
                    body = body.Replace("##Shipping_Logo##", "https://househomelove.com.au/images/app/clickandcollect.jpg");
                    body = body.Replace("##Width_Shipping_Logo##", "150");
                }
                else if(model.Checkout.ShippingMethod == Constant.ShippingMethod.AustraliaPost)
                {
                    body = body.Replace("##DELIVERY_DETAIL##",
                    $"Delivery Address: {model.Checkout.DeliveryAddress}, {model.Checkout.DeliverySuburb}, {model.Checkout.DeliveryState} {model.Checkout.DeliveryPostcode}, {model.Checkout.DeliveryCountry}");
                    body = body.Replace("##Shipping_Logo##", "https://househomelove.com.au/images/app/auspost_logo.png");
                    body = body.Replace("##Width_Shipping_Logo##", "100");
                }
                else
                {
                    body = body.Replace("##DELIVERY_DETAIL##",
                    $"Delivery Address: {model.Checkout.DeliveryAddress}, {model.Checkout.DeliverySuburb}, {model.Checkout.DeliveryState} {model.Checkout.DeliveryPostcode}, {model.Checkout.DeliveryCountry}");
                    body = body.Replace("##Shipping_Logo##", "https://househomelove.com.au/images/app/homemail.png");
                    body = body.Replace("##Width_Shipping_Logo##", "150");
                }

                if (!string.IsNullOrEmpty(model.Checkout.Note))
                {
                    body = body.Replace("##NOTE##", $"Note: {model.Checkout.Note}");
                }
                else
                {
                    body = body.Replace("##NOTE##", string.Empty);
                }

                if (model.ShoppingCart.Discount != 0)
                {
                    body = body.Replace("##DISCOUNT_TITLE##", "DISCOUNT");
                    body = body.Replace("##DISCOUNT_VALUE##", "$" + model.ShoppingCart.Discount.ToString());
                }
                else
                {
                    body = body.Replace("##DISCOUNT_TITLE##", string.Empty);
                    body = body.Replace("##DISCOUNT_VALUE##", string.Empty);
                }

                string cartItems = string.Empty;

                int count = 0;

                foreach (CartItemModel item in model.ShoppingCart.CartItemList)
                {
                    count++;

                    cartItems +=
                        $"<tr><td style=\"text-align: center; font-family:Arial,sans-serif; padding: 15px\">{count}</td><td style=\"text-align: center; font-family:Arial,sans-serif; padding: 15px\">{item.ProductId}</td><td style=\"text-align: center; font-family:Arial,sans-serif; padding: 15px\">{item.Name}</td><td style=\"text-align: center; font-family:Arial,sans-serif; padding: 15px\">{item.Quantity}</td><td style=\"text-align: center; font-family:Arial,sans-serif; padding: 15px\">${item.DiscountPrice}</td><td style=\"text-align: center; font-family:Arial,sans-serif; padding: 15px\">${item.FinalPrice}</td></tr>";
                }

                body = body.Replace("##CART_ITEMS##", cartItems);

                if(emails == null)
                {
                    bool res = SendEmail(appConfig, senderEmail, body, model.Checkout.Email, password, "Order Confirmation");

                    return res;
                }
                else
                {
                    foreach(string email in emails)
                    {
                        SendEmail(appConfig, senderEmail, body, email, password, "Order Confirmation");
                    }

                    return true;
                }
            }

            return false;
        }

        public static bool SendRecoveryPasswordEmail(ApplicationSettingModel appConfig, string email, string link)
        {
            string body = GetEmailTemplate(Constant.EmailType.RecoveryPasswordEmail);
            string senderEmail = ConfigurationManager.AppSettings["EmailUsername"];
            string password = ConfigurationManager.AppSettings["EmailPassword"];

            if (!string.IsNullOrEmpty(body) || !string.IsNullOrEmpty(senderEmail))
            {
                body = body.Replace("###YourName###", email);
                body = body.Replace("###RecoveryLink###", link);

                bool res = SendEmail(appConfig, senderEmail, body, email, password, "Recovery Password");

                return res;
            }

            return false;
        }

        public static bool SendRegistrationEmail(ApplicationSettingModel appConfig, string fullname, string email)
        {
            string body = GetEmailTemplate(Constant.EmailType.RegisterAccountEmail);
            string senderEmail = ConfigurationManager.AppSettings["EmailUsername"];
            string password = ConfigurationManager.AppSettings["EmailPassword"];

            if (!string.IsNullOrEmpty(body) || !string.IsNullOrEmpty(senderEmail))
            {
                body = body.Replace("###YourName###", fullname);
                body = body.Replace("###YourEmail###", email);

                bool res = SendEmail(appConfig, senderEmail, body, email, password, "Thanks for registration");

                return res;
            }

            return false;
        }

        public static bool SendEmail(ApplicationSettingModel appConfig, string senderEmail, string body, string toEmail, string password, string header)
        {
            try
            {
                log.Info($"Sending email in Azure by SendGrid. To: {toEmail} / Header: {header}");

                string apiKey = ConfigurationManager.AppSettings["SendGridAPIKey"];
                SendGridClient client = new SendGridClient(apiKey);

                EmailAddress from = new EmailAddress(senderEmail, "househomelove");
                string subject = $"househomelove - {header}";
                EmailAddress to = new EmailAddress(toEmail, "");

                SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, null, body);

                if (appConfig != null)
                {
                    //if (!string.IsNullOrEmpty(appConfig.CustomerServiceEmail))
                    //{
                    //    msg.AddBcc(appConfig.CustomerServiceEmail);
                    //}

                    //if (!string.IsNullOrEmpty(appConfig.ECommerceEmail))
                    //{
                    //    msg.AddBcc(appConfig.ECommerceEmail);
                    //}

                    if (!string.IsNullOrEmpty(appConfig.PaymentReceiptEmail))
                    {
                        msg.AddBcc(appConfig.PaymentReceiptEmail);
                    }
                }

                Response response = client.SendEmailAsync(msg).Result;

                log.Info($"Sending email to {toEmail} {header} Status: {response.StatusCode}");

                return true;
            }
            catch (Exception ex)
            {
                log.Debug($"Cannot send email from system: {ex.Message}");
                throw new DataLayerException(ex.Message);
            }
        }
    }
}