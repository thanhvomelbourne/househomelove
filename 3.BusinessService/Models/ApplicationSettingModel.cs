using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class ApplicationSettingModel
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string NewslettersDescription { get; set; }
        public string BannerQuote1 { get; set; }
        public string BannerQuote2 { get; set; }
        public string BannerQuote3 { get; set; }
        public string BannerImage1 { get; set; }
        public string BannerImage2 { get; set; }
        public string BannerImage3 { get; set; }
        public string PromotionImage1 { get; set; }
        public string PromotionImage2 { get; set; }
        public string PromotionImage3 { get; set; }
        public string PromotionImage4 { get; set; }
        public string PromotionImage5 { get; set; }
        public string PromotionImage6 { get; set; }
        public string AdsIcon1 { get; set; }
        public string AdsTitle1 { get; set; }
        public string AdsIcon2 { get; set; }
        public string AdsTitle2 { get; set; }
        public string AdsIcon3 { get; set; }
        public string AdsTitle3 { get; set; }
        public string AdsIcon4 { get; set; }
        public string AdsTitle4 { get; set; }
        public string AdsIcon5 { get; set; }
        public string AdsTitle5 { get; set; }
        public string AdsIcon6 { get; set; }
        public string AdsTitle6 { get; set; }
        public string ServiceIcon1 { get; set; }
        public string ServiceTitle1 { get; set; }
        public string ServiceDescription1 { get; set; }
        public string ServiceIcon2 { get; set; }
        public string ServiceTitle2 { get; set; }
        public string ServiceDescription2 { get; set; }
        public string ServiceIcon3 { get; set; }
        public string ServiceTitle3 { get; set; }
        public string ServiceDescription3 { get; set; }
        public string ServiceIcon4 { get; set; }
        public string ServiceTitle4 { get; set; }
        public string ServiceDescription4 { get; set; }
        public string PartnerLogo1 { get; set; }
        public string PartnerLogo2 { get; set; }
        public string PartnerLogo3 { get; set; }
        public string PartnerLogo4 { get; set; }
        public string TermsAndConditions { get; set; }
        public string FAQ { get; set; }
        public string ReturnPolicy { get; set; }
        public string AdminEmail { get; set; }
        public string CustomerServiceEmail { get; set; }
        public string ECommerceEmail { get; set; }
        public string PaymentReceiptEmail { get; set; }
        public double GSTPercent { get; set; }
        public double CreditCardSurcharge { get; set; }
        public double PaypalSurcharge { get; set; }
        public double? FreeShippingAusPostFrom { get; set; }
        public double? FreeShippingHomeMailFrom { get; set; }
        public string AusPostDescription { get; set; }
        public string HomeMailDescription { get; set; }
        public double HomeMailDefaultPrice1 { get; set; }
        public double HomeMailDefaultPrice2 { get; set; }
        public double HomeMailDefaultPrice3 { get; set; }
        public double HomeMailAvailableFrom { get; set; }
        public int WarehousePostcode { get; set; }
        public string PopularSearchTag { get; set; }
        public double? CurrencyRateUSDToAUD { get; set; }
        public double? RateCalculateOriginalPrice { get; set; }
        public double? RateCalculateDiscountPrice { get; set; }
        public string ClickAndCollectDescription { get; set; }
        public string MetaDescription { get; set; }
        public int TotalProductImages { get; set; }
        public int TotalUsedProductImages { get; set; }
        public int TotalUnusedProductImages { get; set; }
        public string TotalProductImagesSize { get; set; }
        public int TotalAppBannerImages { get; set; }
        public int TotalUsedAppBannerImages { get; set; }
        public int TotalUnusedAppBannerImages { get; set; }
        public string TotalAppBannerImagesSize { get; set; }
        public int TotalQRCodeImages { get; set; }
        public int TotalUsedQRCodeImages { get; set; }
        public int TotalUnusedQRCodeImages { get; set; }
        public string TotalQRCodeImagesSize { get; set; }
        public int TotalShoppingCart { get; set; }
        public int TotalUnusedShoppingCart { get; set; }
        public int TotalUsedShoppingCart { get; set; }
        public string AboutUs { get; set; }
        public string ClickAndCollectPage { get; set; }
        public string HomeMailPage { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string DatabaseVersion { get; set; }
        public string ServerInformation { get; set; }
    }
}
