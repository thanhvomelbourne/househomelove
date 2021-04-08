using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models.Common
{
    public class AdminModel
    {
        public List<ContactModel> ContactMessageList;
        public DashboardModel Dashboard;
        public CollectionModel<CategoryModel> CategoryList;
        public CollectionModel<GroupModel> GroupList;
        public CollectionModel<ColorModel> ColorList;
        public CollectionModel<ProductModel> ProductList;
        public CollectionModel<OrderModel> OrderList;
        public CollectionModel<ContactModel> ContactList;
        public CollectionModel<PromotionModel> PromotionList;
        public CollectionModel<SubcriptionModel> SubscriptionList;
        public CollectionModel<NewsletterTemplateModel> NewsletterTemplateList;
        public CollectionModel<UserProfileModel> UserProfileList;
        public CollectionModel<PreOrderModel> PreOrderList;
        public ApplicationSettingModel ApplicationSetting;
        public ApplicationSettingModel OtherContent;
        public CollectionModel<EVoucherModel> EVoucherList;
        public ProductModel ProductDetail;
        public CollectionModel<OurEventModel> OurEventList;
    }
}
