using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class ShoppingCartModel
    {
        public int Id { get; set; }
        public double TotalImportPrice { get; set; }
        public double TotalProfit { get; set; }
        public double TotalAmountExGst { get; set; }
        public double TotalAmountIncGst { get; set; }
        public double Gst { get; set; }
        public double Discount { get; set; }
        public double ShippingFee { get; set; }
        public double TotalFinalPrice { get; set; }
        public string EstimateDelivery { get; set; }
        public int PromotionId { get; set; }
        public int ShippingPromotionId { get; set; }
        public string PromotionCode { get; set; }
        public string ShippingPromotionCode { get; set; }
        public double GSTPercent { get; set; }
        public double CreditCardSurcharge { get; set; }
        public double PaypalSurcharge { get; set; }
        public double? FreeShippingAusPostFrom { get; set; }
        public double? FreeShippingHomeMailFrom { get; set; }
        public double? HomeMailDefaultPrice1 { get; set; }
        public double? HomeMailDefaultPrice2 { get; set; }
        public double? HomeMailDefaultPrice3 { get; set; }
        public double? HomeMailAvailableFrom { get; set; }
        public int? WarehousePostcode { get; set; }
        public bool HomeMailAvailable { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public List<CartItemModel> CartItemList { get; set; }
        public PromotionModel Promotion { get; set; }
    }
}
