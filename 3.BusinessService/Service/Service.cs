using BusinessService.IBusiness;

namespace BusinessService.Service
{
    public class Service : IService
    {
        private readonly ICategoryBusiness _categoryBusiness;
        private readonly IProductBusiness _productBusiness;
        private readonly IApplicationSettingBusiness _applicationSettingBusiness;
        private readonly IShoppingCartBusiness _shoppingCartBusiness;
        private readonly IOrderBusiness _orderBusiness;
        private readonly IUserProfileBusiness _userProfileBusiness;
        private readonly ISubcriptionBusiness _subcriptionBusiness;
        private readonly IContactBusiness _contactBusiness;
        private readonly IPromotionBusiness _promotionBusiness;
        private readonly IEVoucherBusiness _eVoucherBusiness;
        private readonly IPreOrderBusiness _preOrderBusiness;
        private readonly IGroupBusiness _groupBusiness;
        private readonly IColorBusiness _colorBusiness;
        private readonly INewsletterTemplateBusiness _newsletterTemplateBusiness;
        private readonly IOurEventBusiness _ourEventBusiness;

        public Service(ICategoryBusiness categoryBusiness,
            IProductBusiness productBusiness,
            IApplicationSettingBusiness applicationSettingBusiness,
            IShoppingCartBusiness shoppingCartBusiness,
            IOrderBusiness orderBusiness,
            IUserProfileBusiness userProfileBusiness,
            ISubcriptionBusiness subcriptionBusiness,
            IContactBusiness contactBusiness,
            IPromotionBusiness promotionBusiness,
            IEVoucherBusiness eVoucherBusiness,
            IPreOrderBusiness preOrderBusiness,
            IGroupBusiness groupBusiness,
            IColorBusiness colorBusiness,
            INewsletterTemplateBusiness newsletterTemplateBusiness,
            IOurEventBusiness ourEventBusiness)
        {
            _categoryBusiness = categoryBusiness;
            _productBusiness = productBusiness;
            _applicationSettingBusiness = applicationSettingBusiness;
            _shoppingCartBusiness = shoppingCartBusiness;
            _orderBusiness = orderBusiness;
            _userProfileBusiness = userProfileBusiness;
            _subcriptionBusiness = subcriptionBusiness;
            _contactBusiness = contactBusiness;
            _promotionBusiness = promotionBusiness;
            _eVoucherBusiness = eVoucherBusiness;
            _preOrderBusiness = preOrderBusiness;
            _groupBusiness = groupBusiness;
            _colorBusiness = colorBusiness;
            _newsletterTemplateBusiness = newsletterTemplateBusiness;
            _ourEventBusiness = ourEventBusiness;
        }

        public ICategoryBusiness Category
        {
            get { return _categoryBusiness; }
        }

        public IProductBusiness Product
        {
            get { return _productBusiness; }
        }

        public IApplicationSettingBusiness ApplicationSetting
        {
            get { return _applicationSettingBusiness; }
        }

        public IShoppingCartBusiness ShoppingCart
        {
            get { return _shoppingCartBusiness; }
        }

        public IOrderBusiness Order
        {
            get { return _orderBusiness; }
        }

        public ISubcriptionBusiness Subcription
        {
            get { return _subcriptionBusiness; }
        }

        public IUserProfileBusiness UserProfile
        {
            get { return _userProfileBusiness; }
        }

        public IContactBusiness Contact
        {
            get { return _contactBusiness; }
        }

        public IPromotionBusiness Promotion
        {
            get { return _promotionBusiness; }
        }

        public IEVoucherBusiness EVoucher
        {
            get { return _eVoucherBusiness; }
        }

        public IPreOrderBusiness PreOrder
        {
            get { return _preOrderBusiness; }
        }

        public IGroupBusiness Group
        {
            get { return _groupBusiness; }
        }

        public IColorBusiness Color
        {
            get { return _colorBusiness; }
        }

        public INewsletterTemplateBusiness NewsletterTemplate
        {
            get { return _newsletterTemplateBusiness; }
        }

        public IOurEventBusiness OurEvent
        {
            get { return _ourEventBusiness; }
        }
    }
}
