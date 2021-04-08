using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string OriginalProductCode { get; set; }
        public string Namekey { get; set; }
        public string ColorCode { get; set; }
        public string ColorCode1 { get; set; }
        public string ColorCode2 { get; set; }
        public string ProductCode { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? SubCategoryId1 { get; set; }
        public string SubCategoryName1 { get; set; }
        public int? SubCategoryId2 { get; set; }
        public string SubCategoryName2 { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MoreInfo { get; set; }
        public string Unit { get; set; }
        public string UnitOfPrice { get; set; }
        public double? ImportPriceUSD { get; set; }
        public double ImportPrice { get; set; }
        public double OriginalPrice { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double DiscountPrice { get; set; }
        public string InStock { get; set; }
        public int StockLevel { get; set; }
        public bool IsLive { get; set; }
        public int NumberOfRating { get; set; }
        public int NumberOfLiked { get; set; }
        public double AverageRating { get; set; }
        public string PrimaryImage { get; set; }
        public string SubImage1 { get; set; }
        public string SubImage2 { get; set; }
        public string SubImage3 { get; set; }
        public string SubImage4 { get; set; }
        public string SubImage5 { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public List<LikedProductModel> LikedProduct { get; set; }
        public bool IsInWishlist { get; set; }
        public bool IsLikedForCurrentUser { get; set; }
        public bool IsReviewedForCurrentUser { get; set; }
        public List<ReviewedProductModel> ReviewedProduct { get; set; }
        public int NumberOfReviewed { get; set; }
        public RatingStatisticModel RatingStatistic { get; set; }
        public double? ExchangeRate { get; set; }
        public double? OriginalRate { get; set; }
        public double? DiscountRate { get; set; }
        public string Materials { get; set; }
        public string Colors { get; set; }
        public string QRCode { get; set; }
        public int? GroupSizeId { get; set; }
        public int? GroupColorId { get; set; }
        public string SizeName { get; set; }
        public int NextProductId { get; set; }
        public int PreviousProductId { get; set; }
    }
}
