using BusinessService.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Service
{
    public interface IService : CoreLibrary.Service.IService
    {
        ICategoryBusiness Category { get; }
        IProductBusiness Product { get; }
        IApplicationSettingBusiness ApplicationSetting { get; }
        IShoppingCartBusiness ShoppingCart { get; }
        IOrderBusiness Order { get; }
        ISubcriptionBusiness Subcription { get; }
        IUserProfileBusiness UserProfile { get; }
        IContactBusiness Contact { get; }
        IPromotionBusiness Promotion { get; }
        IEVoucherBusiness EVoucher { get; }
        IPreOrderBusiness PreOrder { get; }
        IGroupBusiness Group { get; }
        IColorBusiness Color { get; }
        INewsletterTemplateBusiness NewsletterTemplate { get; }
        IOurEventBusiness OurEvent { get; }
    }
}
