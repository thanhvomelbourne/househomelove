using BusinessService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.IBusiness
{
    public interface IProductBusiness
    {
        CollectionModel<ProductModel> GetAllProductsByFilter(Parameter parameter);
        CollectionModel<ProductModel> GetAllProductsByFilterForAdmin(Parameter parameter);
        IList<ProductModel> GetAllProducts();
        IList<ProductModel> GetLatestProductsForHomePage();
        IList<WishlistModel> GetAllWishlistProducts(int userId);
        IList<ProductModel> GetRelatedProductsForDetailPage(int categoryId);
        ProductModel Insert(ProductModel category);
        ProductModel Update(ProductModel category);
        ProductModel UpdateQRCode(ProductModel category);
        ProductModel Details(int id, int userId = 0);
        string GenerateNameKey(int catId);
        bool Delete(int id);
        bool DeleteWishlist(int wishlistId);
        bool AddProductToWishlist(int userId, int productId);
        ProductModel LikeProduct(int userId, int productId, out bool result);
        ProductModel ReviewProduct(int userId, int productId, int rating, string comment);
        bool RemoveReviewProduct(int id, int userId, int productId);
        Task<bool> Delete(List<int> ids);
        void ApplyDiscount(int categoryId, double percent, bool applyForSubCategory);
        IList<ProductModel> GetProductsInSameGroup(int groupId, int productId);
        bool CheckValidate(string productName);
    }
}
