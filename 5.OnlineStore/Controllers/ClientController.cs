using BusinessService.Models;
using BusinessService.Models.Common;
using BusinessService.Service;
using CoreLibrary.Data;
using OnlineStore.Helper;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace OnlineStore.Controllers
{
    public class ClientController : Controller
    {
        private readonly IService _service;
        readonly HomePageModel _homepageModel;

        public ClientController(IService service)
        {
            _service = service;
            _homepageModel = _service.ApplicationSetting.GetCommonDataForHomePageClient();
        }

        [Route]
        public ActionResult Index()
        {
            if (!WebSecurity.IsAuthenticated)
            {
                UtilityHelper.SetCookie(UtilityHelper.USER_ID, null, DateTime.UtcNow.AddDays(1));
            }
            _homepageModel.LatestProductList = _service.Product.GetLatestProductsForHomePage();
            return View(_homepageModel);
        }

        [Route("login")]
        public ActionResult Login()
        {
            return View(_homepageModel);
        }

        [Route("myaccount")]
        public ActionResult MyAccount()
        {
            if (WebSecurity.IsAuthenticated)
            {
                _homepageModel.CurrentUserLogin = _service.UserProfile.Details(WebSecurity.CurrentUserId);

                return View(_homepageModel);
            }
            else
            {
                return RedirectToAction("Login", "Client");
            }
        }

        [Route("register")]
        public ActionResult Register()
        {
            return View(_homepageModel);
        }

        [Route("contact")]
        public ActionResult Contact()
        {
            return View(_homepageModel);
        }

        [Route("myaccount/historytransaction")]
        public ActionResult HistoryTransaction()
        {
            if (WebSecurity.IsAuthenticated)
            {
                _homepageModel.HistoryTransaction = _service.Order.GetHistoryTransaction(WebSecurity.CurrentUserId);
                return View(_homepageModel);
            }
            else
            {
                return RedirectToAction("Login", "Client");
            }
        }

        [Route("myaccount/wishlist")]
        public ActionResult Wishlist()
        {
            if (WebSecurity.IsAuthenticated)
            {
                _homepageModel.Wishlist = _service.Product.GetAllWishlistProducts(WebSecurity.CurrentUserId).ToList();
                return View(_homepageModel);
            }
            else
            {
                return RedirectToAction("Login", "Client");
            }
        }

        [Route("myaccount/changepassword")]
        public ActionResult ChangePassword()
        {
            if (WebSecurity.IsAuthenticated)
            {
                _homepageModel.CurrentUserLogin = _service.UserProfile.Details(WebSecurity.CurrentUserId);

                return View(_homepageModel);
            }
            else
            {
                return RedirectToAction("Login", "Client");
            }
        }

        [Route("shop")]
        public ActionResult Products(Parameter parameter)
        {
            parameter.PageNo = parameter.PageNo != 0 ? parameter.PageNo : 1;

            if (parameter.Search == null)
            {
                parameter.Search = parameter.CurrentSearch;
            }

            if(Math.Abs(parameter.MaxPrice) <= 0)
            {
                parameter.MaxPrice = 1000;
            }

            ViewBag.CurrentSearch = parameter.Search;

            if (Request.Cookies[UtilityHelper.ITEMS_PER_PAGE] != null)
            {
                var value = Request.Cookies[UtilityHelper.ITEMS_PER_PAGE].Value;

                if (value == Constant.CommonValue.PageSize20.ToString())
                {
                    parameter.PageSize = Constant.CommonValue.PageSize20;
                }
                else if (value == Constant.CommonValue.PageSize40.ToString())
                {
                    parameter.PageSize = Constant.CommonValue.PageSize40;
                }
                else
                {
                    parameter.PageSize = Constant.CommonValue.PageSize20;
                }
            }
            else
            {
                UtilityHelper.SetCookie(UtilityHelper.ITEMS_PER_PAGE, Constant.CommonValue.PageSize20.ToString(), DateTime.UtcNow.AddDays(100));
                parameter.PageSize = Constant.CommonValue.PageSize20;
            }

            if (Request.Cookies[UtilityHelper.SORT_BY] != null)
            {
                var value = Request.Cookies[UtilityHelper.SORT_BY].Value;

                if (value == Constant.CommonValue.SortByNewest)
                {
                    parameter.SortBy = Constant.CommonValue.SortByNewest;
                }
                else if (value == Constant.CommonValue.SortByPriceLowToHigh)
                {
                    parameter.SortBy = Constant.CommonValue.SortByPriceLowToHigh;
                }
                else if (value == Constant.CommonValue.SortByPriceHighToLow)
                {
                    parameter.SortBy = Constant.CommonValue.SortByPriceHighToLow;
                }
                else
                {
                    parameter.SortBy = Constant.CommonValue.SortByNewest;
                }
            }
            else
            {
                UtilityHelper.SetCookie(UtilityHelper.SORT_BY, Constant.CommonValue.SortByNewest, DateTime.UtcNow.AddDays(100));
                parameter.SortBy = Constant.CommonValue.SortByNewest;
            }

            _homepageModel.ProductList = _service.Product.GetAllProductsByFilter(parameter);

            return View(_homepageModel);
        }

        [Route("shop/product")]
        public ActionResult ProductDetail()
        {
            string productId = Request.QueryString["productId"] != null ? Request.QueryString["productId"].ToString() : "";

            if (!string.IsNullOrEmpty(productId))
            {
                ProductModel data = new ProductModel();

                if (WebSecurity.IsAuthenticated)
                {
                    data = _service.Product.Details(Convert.ToInt32(productId), WebSecurity.CurrentUserId);
                }
                else
                {
                    data = _service.Product.Details(Convert.ToInt32(productId));
                }

                _homepageModel.CurrentProductDetail = data;
                _homepageModel.RelatedProductList = _service.Product.GetRelatedProductsForDetailPage(_homepageModel.CurrentProductDetail.CategoryId).ToList();

                if (_homepageModel.CurrentProductDetail.GroupSizeId.HasValue)
                {
                    _homepageModel.ProductsInSameGroupSize = _service.Product.GetProductsInSameGroup(_homepageModel.CurrentProductDetail.GroupSizeId.Value, Convert.ToInt32(productId));
                }
                if (_homepageModel.CurrentProductDetail.GroupColorId.HasValue)
                {
                    _homepageModel.ProductsInSameGroupColor = _service.Product.GetProductsInSameGroup(_homepageModel.CurrentProductDetail.GroupColorId.Value, Convert.ToInt32(productId));
                }
            }

            if (WebSecurity.IsAuthenticated)
            {
                _homepageModel.CurrentUserLogin = _service.UserProfile.Details(WebSecurity.CurrentUserId);
            }

            return View(_homepageModel);
        }

        [Route("shop/shoppingcart")]
        public ActionResult ShoppingCart()
        {
            int cartId = Convert.ToInt32(UtilityHelper.GetCookie(UtilityHelper.SHOPPING_CART_ID, true));

            if (cartId == 0)
            {
                return RedirectToAction("Products", "Client");
            }

            return View(_homepageModel);
        }

        [Route("shop/checkout")]
        public ActionResult Checkout()
        {
            int cartId = Convert.ToInt32(UtilityHelper.GetCookie(UtilityHelper.SHOPPING_CART_ID, true));

            if (cartId == 0)
            {
                return RedirectToAction("Products", "Client");
            }

            return View(_homepageModel);
        }

        [Route("shop/checkout/orderconfirmation")]
        public ActionResult OrderConfirmation()
        {
            return View(_homepageModel);
        }

        [Route("shop/checkout/paypalreturn")]
        public ActionResult PayPalReturn()
        {
            return View(_homepageModel);
        }

        [Route("clickandcollect")]
        public ActionResult ClickAndCollectLanding()
        {
            return View(_homepageModel);
        }

        [Route("zippaylanding")]
        public ActionResult ZipPayLanding()
        {
            return View(_homepageModel);
        }

        [Route("homemaillanding")]
        public ActionResult HomeMailLanding()
        {
            return View(_homepageModel);
        }

        [Route("termsandconditions")]
        public ActionResult TermsAndConditions()
        {
            return View(_homepageModel);
        }

        [Route("faq")]
        public ActionResult FAQ()
        {
            return View(_homepageModel);
        }

        [Route("returnpolicy")]
        public ActionResult ReturnPolicy()
        {
            return View(_homepageModel);
        }

        [Route("error")]
        public ActionResult Error()
        {
            return View(_homepageModel);
        }

        [Route("aboutus")]
        public ActionResult AboutUs()
        {
            return View(_homepageModel);
        }

        [Route("ourevents")]
        public ActionResult OurEvent()
        {
            _homepageModel.OurEventList = _service.OurEvent.GetAllOurEvents();
            return View(_homepageModel);
        }
    }
}