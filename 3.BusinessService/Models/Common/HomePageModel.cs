using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models.Common
{
    public class HomePageModel
    {
        //Using for application setting
        public ApplicationSettingModel ApplicationSetting;

        //Using for menu on layout
        public List<CategoryModel> CategoryList;

        //Using for homepage
        public IList<ProductModel> LatestProductList;

        //Using for product page
        public CollectionModel<ProductModel> ProductList;

        //Using for product detail page
        public ProductModel CurrentProductDetail;

        public IList<ProductModel> ProductsInSameGroupSize;

        public IList<ProductModel> ProductsInSameGroupColor;

        //Using for history transaction
        public List<OrderModel> HistoryTransaction;

        public List<ProductModel> RelatedProductList;

        public UserProfileModel CurrentUserLogin;

        public string[] PopularSearchs;

        public List<WishlistModel> Wishlist;

        //Using for event page
        public IList<OurEventModel> OurEventList;
    }
}
