using BusinessService.Models;
using BusinessService.Models.Common;
using BusinessService.Service;
using CoreLibrary.Data;
using OnlineStore.Helper;
using System;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly IService _service;
        readonly AdminModel _adminModel;

        public AdminController(IService service)
        {
            this._service = service;
            _adminModel = _service.ApplicationSetting.GetCommonDataForAdminPage();
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Index()
        {
            _adminModel.Dashboard = _service.ApplicationSetting.GetDashboard();

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Category(Parameter parameter)
        {
            parameter.PageNo = parameter.PageNo != 0 ? parameter.PageNo : 1;

            if (parameter.Search == null)
            {
                parameter.Search = parameter.CurrentSearch;
            }

            ViewBag.SearchByLive = parameter.SearchByLive;
            ViewBag.CurrentSearch = parameter.Search;

            UtilityHelper.SetCookie(UtilityHelper.CURRENT_CATEGORY_SEARCH_ISLIVE_FOR_ADMIN, !string.IsNullOrEmpty(parameter.SearchByLive) ? parameter.SearchByLive.Replace(' ', '+') : string.Empty, DateTime.UtcNow.AddDays(1));

            _adminModel.CategoryList = _service.Category.GetAllCategoriesByFilter(parameter);

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Group(Parameter parameter)
        {
            parameter.PageNo = parameter.PageNo != 0 ? parameter.PageNo : 1;

            if (parameter.Search == null)
            {
                parameter.Search = parameter.CurrentSearch;
            }
            
            ViewBag.CurrentSearch = parameter.Search;
            
            _adminModel.GroupList = _service.Group.GetAllGroupsByFilter(parameter);

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Color(Parameter parameter)
        {
            parameter.PageNo = parameter.PageNo != 0 ? parameter.PageNo : 1;

            if (parameter.Search == null)
            {
                parameter.Search = parameter.CurrentSearch;
            }

            ViewBag.CurrentSearch = parameter.Search;

            _adminModel.ColorList = _service.Color.GetAllColorsByFilter(parameter);

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Contact(Parameter parameter)
        {
            parameter.PageNo = parameter.PageNo != 0 ? parameter.PageNo : 1;

            if (parameter.Search == null)
            {
                parameter.Search = parameter.CurrentSearch;
            }

            ViewBag.CurrentSearch = parameter.Search;

            _adminModel.ContactList = _service.Contact.GetAllContactsByFilter(parameter);

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Product(Parameter parameter)
        {
            parameter.PageNo = parameter.PageNo != 0 ? parameter.PageNo : 1;

            if (parameter.Search == null)
            {
                parameter.Search = parameter.CurrentSearch;
            }

            if (Math.Abs(parameter.MaxPrice) <= 0)
            {
                parameter.MaxPrice = 500;
            }

            ViewBag.SearchByLive = parameter.SearchByLive;
            ViewBag.CurrentSearch = parameter.Search;
            ViewBag.SearchByStockLevel = parameter.SearchByStockLevel;

            if (parameter.PageSize == 0)
            {
                parameter.PageSize = Constant.CommonValue.PageSize20;
            }

            UtilityHelper.SetCookie(UtilityHelper.CURRENT_PRODUCT_PAGE_FOR_ADMIN, parameter.PageNo.ToString(), DateTime.UtcNow.AddDays(1));
            UtilityHelper.SetCookie(UtilityHelper.CURRENT_PRODUCT_SEARCH_FOR_ADMIN, !string.IsNullOrEmpty(parameter.Search) ? parameter.Search.Replace(' ', '+') : string.Empty, DateTime.UtcNow.AddDays(1));
            UtilityHelper.SetCookie(UtilityHelper.CURRENT_PRODUCT_SEARCH_ISLIVE_FOR_ADMIN, !string.IsNullOrEmpty(parameter.SearchByLive) ? parameter.SearchByLive.Replace(' ', '+') : string.Empty, DateTime.UtcNow.AddDays(1));
            UtilityHelper.SetCookie(UtilityHelper.CURRENT_PRODUCT_SEARCH_INSTOCK_FOR_ADMIN, !string.IsNullOrEmpty(parameter.SearchByStockLevel) ? parameter.SearchByStockLevel.Replace(' ', '+') : string.Empty, DateTime.UtcNow.AddDays(1));

            _adminModel.ProductList = _service.Product.GetAllProductsByFilterForAdmin(parameter);

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult ProductDetail()

        {
            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult OrderDetail()

        {
            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Order(Parameter parameter)
        {
            parameter.PageNo = parameter.PageNo != 0 ? parameter.PageNo : 1;

            if (parameter.Search == null)
            {
                parameter.Search = parameter.CurrentSearch;
            }

            ViewBag.CurrentSearch = parameter.Search;
            ViewBag.SearchByConfirmedStatus = parameter.SearchByConfirmedStatus;
            ViewBag.SearchByShippingMethod = parameter.SearchByShippingMethod;

            UtilityHelper.SetCookie(UtilityHelper.CURRENT_ORDER_PAGE_FOR_ADMIN, parameter.PageNo.ToString(), DateTime.UtcNow.AddDays(1));
            UtilityHelper.SetCookie(UtilityHelper.CURRENT_ORDER_SEARCH_FOR_ADMIN, !string.IsNullOrEmpty(parameter.Search) ? parameter.Search.Replace(' ', '+') : string.Empty, DateTime.UtcNow.AddDays(1));
            UtilityHelper.SetCookie(UtilityHelper.CURRENT_ORDER_SEARCH_CONFIRMED_STATUS_FOR_ADMIN, !string.IsNullOrEmpty(parameter.SearchByConfirmedStatus) ? parameter.SearchByConfirmedStatus.Replace(' ', '+') : string.Empty, DateTime.UtcNow.AddDays(1));
            UtilityHelper.SetCookie(UtilityHelper.CURRENT_ORDER_SEARCH_SHIPPING_METHOD_FOR_ADMIN, !string.IsNullOrEmpty(parameter.SearchByShippingMethod) ? parameter.SearchByShippingMethod.Replace(' ', '+') : string.Empty, DateTime.UtcNow.AddDays(1));

            _adminModel.OrderList = _service.Order.GetAllOrdersByFilter(parameter);

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult ContentSetting()
        {
            _adminModel.ApplicationSetting = _service.ApplicationSetting.Details();

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult AppConfiguration()
        {
            _adminModel.ApplicationSetting = _service.ApplicationSetting.AppConfiguration();

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult UserProfile(Parameter parameter)
        {
            parameter.PageNo = parameter.PageNo != 0 ? parameter.PageNo : 1;

            if (parameter.Search == null)
            {
                parameter.Search = parameter.CurrentSearch;
            }

            ViewBag.CurrentSearch = parameter.Search;

            _adminModel.UserProfileList = _service.UserProfile.GetAllUsersByFilter(parameter);

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult EVoucher(Parameter parameter)
        {
            parameter.PageNo = parameter.PageNo != 0 ? parameter.PageNo : 1;

            if (parameter.Search == null)
            {
                parameter.Search = parameter.CurrentSearch;
            }

            ViewBag.CurrentSearch = parameter.Search;

            _adminModel.EVoucherList = _service.EVoucher.GetAllEVouchersByFilter(parameter);

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Promotion(Parameter parameter)
        {
            parameter.PageNo = parameter.PageNo != 0 ? parameter.PageNo : 1;

            if (parameter.Search == null)
            {
                parameter.Search = parameter.CurrentSearch;
            }

            ViewBag.CurrentSearch = parameter.Search;

            _adminModel.PromotionList = _service.Promotion.GetAllPromotionsByFilter(parameter);

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Subscription(Parameter parameter)
        {
            parameter.PageNo = parameter.PageNo != 0 ? parameter.PageNo : 1;

            if (parameter.Search == null)
            {
                parameter.Search = parameter.CurrentSearch;
            }

            ViewBag.CurrentSearch = parameter.Search;

            _adminModel.SubscriptionList = _service.Subcription.GetAllSubscriptionsByFilter(parameter);

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult NewsletterTemplate(Parameter parameter)
        {
            parameter.PageNo = parameter.PageNo != 0 ? parameter.PageNo : 1;

            if (parameter.Search == null)
            {
                parameter.Search = parameter.CurrentSearch;
            }

            ViewBag.CurrentSearch = parameter.Search;

            _adminModel.NewsletterTemplateList = _service.NewsletterTemplate.GetAllNewsletterTemplateByFilter(parameter);

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult PolicySetting(Parameter parameter)
        {
            _adminModel.OtherContent = _service.ApplicationSetting.PolicySetting();

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult PreOrder(Parameter parameter)
        {
            parameter.PageNo = parameter.PageNo != 0 ? parameter.PageNo : 1;

            if (parameter.Search == null)
            {
                parameter.Search = parameter.CurrentSearch;
            }

            ViewBag.CurrentSearch = parameter.Search;

            _adminModel.PreOrderList = _service.PreOrder.GetAllPreOrdersByFilter(parameter);

            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Report()
        {
            return View(_adminModel);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Event(Parameter parameter)
        {
            parameter.PageNo = parameter.PageNo != 0 ? parameter.PageNo : 1;

            if (parameter.Search == null)
            {
                parameter.Search = parameter.CurrentSearch;
            }

            ViewBag.CurrentSearch = parameter.Search;

            _adminModel.OurEventList = _service.OurEvent.GetAllOurEventsByFilter(parameter);

            return View(_adminModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
    }
}